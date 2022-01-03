using EPSMGest.Models;
using EPSMGest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
    [Route("Departamento/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class DepartamentoController : Controller
    {
        private IDepartamentoService DepartamentoService { get; set; }

        public DepartamentoController(IDepartamentoService _departamentoService)
        {
            DepartamentoService = _departamentoService;
        }

        public IActionResult Index()
        {
            var model = DepartamentoService.GetDepartamentos();
            return View(model);
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartamentoModel model)
        {
            DepartamentoService.CreateDepartamento(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Editar/{id}")]
        public IActionResult Edit(int id)
        {
            var model = DepartamentoService.GetDepartamento(id);
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartamentoModel model)
        {
            DepartamentoService.EditDepartamento(model);
            TempData["Sucess"] = "Departamento editado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(int id)
        {
            bool flag = DepartamentoService.DeleteDepartamento(id);
            if (flag)
                TempData["Sucess"] = "Departamento apagada com sucesso!";
            else
            {
                TempData["Error"] = "Departamento  não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
                return RedirectToAction("Detalhes", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}
