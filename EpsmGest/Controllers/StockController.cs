using EpsmGest.Services.Stock;
using EpsmGest.ViewModel;
using EPSMGest.Models.Stocks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
	[Authorize]
	[Route("Stock/")]
	public class StockController : Controller
	{
		private IStockService StockService { get; set; }

		public StockController(IStockService _stockService)
		{
			StockService = _stockService;
		}

		[HttpGet]
		[Route(""), Route("Index")]
		public IActionResult Index()
		{
			return View(StockService.GetStocks());
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			ViewBag.Categories = StockService.GetCategoryIds();
			return View();
		}

		[HttpPost]
		[Route("Create")]
		public IActionResult Create(CreateSockViewModel model)
		{
			StockService.CreateStock(model);
			TempData["Success"] = "Novo Item adicionado!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(string id)
		{
			if (int.TryParse(id, out var stockId))
			{
				ViewBag.categories = StockService.GetCategoryIds();
				return View(StockService.GetStock(stockId));
			}
			TempData["Error"] = "Item do estoque não encontrado";
			return RedirectToAction("Index");
		}

		[HttpPost]
		[Route("Edit")]
		public IActionResult Edit(StockModel model)
		{
			if (StockService.EditStock(model))
				TempData["Success"] = "Item editado!";
			else
				TempData["Error"] = "Não foi possivel encontrar o Item";
			return RedirectToAction("Details", new { id = model.Id });
		}

		[HttpGet]
		[Route("Delete/{id}")]
		public IActionResult Delete(string id)
		{
			if (int.TryParse(id, out var vehicleId))
			{
				if (StockService.RemoveStock(vehicleId))
				{
					TempData["Success"] = "Item apagado.";
					return RedirectToAction("Index");
				}
			}
			TempData["Error"] = "Não foi possivel encontrar o Item";
			return RedirectToAction("Details", new { id });
		}
	}
}
