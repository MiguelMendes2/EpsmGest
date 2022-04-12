using EpsmGest.ViewModel;
using EPSMGest.Models.Stocks;

namespace EpsmGest.Services.Stock
{
	public interface IStockService
	{
		public IEnumerable<StockCategoryModel> GetStocks();

		public StockModel? GetStock(int id);

		public void CreateStock(CreateSockViewModel model);

		public bool EditStock(StockModel model);

		public bool RemoveStock(int id);

		// --- Stock Category ---

		public int CreateCategory(string Name);

		public List<DropdownViewModel> GetCategoryIds();
	}
}
