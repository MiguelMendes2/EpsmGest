using EPSMGest.Models;
using EPSMGest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
    [Route("Departamento/")]
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

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentModel model)
        {
            DepartamentoService.CreateDepartment(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Editar/{id}")]
        public IActionResult Edit(int id)
        {
            var model = DepartamentoService.GetDepartment(id);
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentModel model)
        {
            DepartamentoService.EditDepartment(model);
            TempData["Sucess"] = "Departamento editado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(int id)
        {
            bool flag = DepartamentoService.DeleteDepartment(id);
            if (flag)
                TempData["Success"] = "Departamento apagada com sucesso!";
            else
            {
                TempData["Error"] = "Departamento  não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
                return RedirectToAction("Detalhes", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}
