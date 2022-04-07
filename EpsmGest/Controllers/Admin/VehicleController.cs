using EpsmGest.Services.Vehicle;
using EPSMGest.Models.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers.Admin
{
	[Route("Vehicle/")]
	[Authorize(Roles = "Administrator,Funcionário DGAR")]
	public class VehicleController : Controller
	{
		private IVehicleService VehicleService { get; set; }

		public VehicleController(IVehicleService _vehicleService)
		{
			VehicleService = _vehicleService;
		}

		[Route("Index"),Route("")]
		public IActionResult Index()
		{
			var model = VehicleService.GetVehicles();
			return View(model);
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Create")]
		public IActionResult Create(VehicleModel model)
		{
			VehicleService.CreateVehicle(model);
			TempData["Success"] = "Veiculo criado!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		[ Route("Details/{id}")]
		public IActionResult Details(string id)
		{
			if (int.TryParse(id, out int result))
			{
				var model = VehicleService.GetVehicle(result);
				return View(model);
			}
			TempData["Error"] = "Veiculo não foi encontrado";
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Edit")]
		public IActionResult Edit(VehicleModel model)
		{
			if (VehicleService.EditVehicle(model))
				TempData["Success"] = "Veiculo editado com sucesso!";
			else
				TempData["Error"] = "Veiculo não foi editado";
			return RedirectToAction("Details", new { id = model.Id });
		}

		[HttpGet]
		[Route("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			bool flag = VehicleService.RemoveVehicle(id);
			if (flag)
			{
				TempData["Success"] = "Veiculo apagado com sucesso!";
				return RedirectToAction("Index");
			}
			TempData["Error"] = "Veiculo  não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
			return RedirectToAction("Details", new { id });
		}
	}
}
