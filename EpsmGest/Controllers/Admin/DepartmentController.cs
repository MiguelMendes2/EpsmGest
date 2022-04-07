using EpsmGest.Services.Department;
using EPSMGest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers.Admin
{
	[Route("Department/")]
	[Authorize(Roles = "Administrator,Funcionário DGAR")]
	public class DepartmentController : Controller
	{
		private IDepartmentService DepartamentoService { get; set; }

		public DepartmentController(IDepartmentService _departamentoService)
		{
			DepartamentoService = _departamentoService;
		}

		public IActionResult Index()
		{
			var model = DepartamentoService.GetDepartments();
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
		public IActionResult Create(DepartmentModel model)
		{
			DepartamentoService.CreateDepartment(model);
			TempData["Success"] = "Departamento criado!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("Details/{id}")]
		public IActionResult Details(string id)
		{
			if (int.TryParse(id, out int result))
			{
				var model = DepartamentoService.GetDepartment(result);
				return View(model);
			}
			TempData["Error"] = "Departamento não foi encontrado";
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Edit")]
		public IActionResult Edit(DepartmentModel model)
		{
			if (DepartamentoService.EditDepartment(model))
				TempData["Success"] = "Departamento editado com sucesso!";
			else
				TempData["Error"] = "Departamento não foi editado";
			return RedirectToAction("Detalhes", new { id = model.DepartamentId });
		}

		[HttpGet]
		[Route("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			bool flag = DepartamentoService.DeleteDepartment(id);
			if (flag)
			{
				TempData["Success"] = "Departamento apagada com sucesso!";
				return RedirectToAction("Index");
			}
			TempData["Error"] = "Departamento  não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
			return RedirectToAction("Detalhes", new { id });
		}
	}
}
