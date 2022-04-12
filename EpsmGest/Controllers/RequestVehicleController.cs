using EpsmGest.Services.Vehicle;
using EPSMGest.Models.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
	[Authorize]
	[Route("RequestVehicle/")]	
	public class RequestVehicleController : Controller
	{
		private IVehicleService VehicleService { get; set; }

		public RequestVehicleController(IVehicleService _vehicleService)
		{
			VehicleService = _vehicleService;
		}

		[HttpGet]
		[Route(""),Route("Index")]
		public IActionResult Index()
		{
			return View(VehicleService.GetRequestVehicles());
		}

		[HttpGet]
		[Route("Historical")]
		public IActionResult Historical()
		{
			return View(VehicleService.GetHistoricRequestVehicles());
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(string id)
		{
			if (Int32.TryParse(id, out var vehicleId))
			{
				ViewBag.Vehicles = VehicleService.GetVehiclesIds();
				return View(VehicleService.GetRequestVehicle(vehicleId));
			}
				
			TempData["Error"] = "Requesição de veiculo não encontrada";
			return RedirectToAction("Index");
		}

		[HttpPost]
		[Route("Edit")]
		public IActionResult Edit(RequestVehicleModel model)
		{
			if (VehicleService.EditRequestVehicle(model))
				TempData["Success"] = "Requesição Veículo editada!";
			else
				TempData["Error"] = "Não foi possivel editar a requesição de veículo";
			return RedirectToAction("Details", new { id = model.Id });
		}

		[HttpGet]
		[Route("ChangeApproval/{id}")]
		public IActionResult ChangeApproval(string id)
		{
			if (Int32.TryParse(id, out var vehicleId))
			{
				if (VehicleService.ChangeAproval(vehicleId))
				{
					TempData["Success"] = "Aprovação da requesição de veiculo alterada";
					return RedirectToAction("Details", new { id = id });
				}
			}
			TempData["Error"] = "Requesição de veículo não encontrada";
			return RedirectToAction("Details", new { id = id });
		}

		[HttpGet]
		[Route("Delete/{id}")]
		public IActionResult Delete(string id)
		{
			if (Int32.TryParse(id, out var vehicleId))
			{
				if (VehicleService.RemoveRequestVehicle(vehicleId))
				{
					TempData["Success"] = "Requesição de veiculo apagada.";
					return RedirectToAction("Index");
				}
			}
			TempData["Error"] = "Requesição de veiculo não encontrada";
			return RedirectToAction("Details", new { id = id });
		}
	}
}
