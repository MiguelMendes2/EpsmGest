using EpsmGest.Data;
using EpsmGest.ViewModel;
using EPSMGest.Models.Stocks;
using Microsoft.EntityFrameworkCore;

namespace EpsmGest.Services.Stock
{
	public class StockService : IStockService
	{
		public readonly EpsmGestDbContext AppDb;

		public StockService(EpsmGestDbContext Db)
		{
			AppDb = Db;
		}

		public IEnumerable<StockCategoryModel> GetStocks()
		{
			return AppDb.StockCategory.Include(x => x.Stocks);
		}

		// --- Stock ---

		public StockModel? GetStock(int id)
		{
			return AppDb.Stock.Include(x => x.StockCategory).FirstOrDefault(x => x.Id == id);
		}

		public void CreateStock(CreateSockViewModel model)
		{
			var categId = CreateCategory(model.CategoryName);
			StockModel stock = new() { Name = model.Name, Quantity = model.Quantity, CategoryId = categId };
			AppDb.Stock.Add(stock);
			AppDb.SaveChanges();
		}

		public bool EditStock(StockModel model)
		{
			var stock = AppDb.Stock.FirstOrDefault(x => x.Id == model.Id);
			if(stock == null)
				return false;
			stock.Name = model.Name;
			stock.Quantity = model.Quantity;
			stock.CategoryId = model.CategoryId;
			AppDb.SaveChanges();
			DeleteUsedCategorys();
			return true;
		}

		public bool RemoveStock(int id)
		{
			var stock = AppDb.Stock.FirstOrDefault(x => x.Id == id);
			if (stock == null)
				return false;
			AppDb.Stock.Remove(stock);
			AppDb.SaveChanges();
			return true;
		}


		// --- Stock Category ---

		public int CreateCategory(string Name)
		{
			var category = AppDb.StockCategory.FirstOrDefault(x => x.Name == Name);
			if (category != null)
				return category.Id;
			StockCategoryModel model = new() { Name = Name };
			AppDb.StockCategory.Add(model);
			AppDb.SaveChanges();
			return model.Id;
		}

		public List<DropdownViewModel> GetCategoryIds()
		{
			return AppDb.StockCategory.Select(x => new DropdownViewModel { Id = x.Id.ToString(), Name = x.Name }).ToList();
		}

		public void DeleteUsedCategorys()
		{
			var categories = AppDb.StockCategory.Include(x => x.Stocks).Where(x => x.Stocks.Any() == false);
			if (categories.Any())
			{
				AppDb.RemoveRange(categories);
				AppDb.SaveChanges();
			}			
		}
	}
}
