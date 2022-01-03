using EPSMGest.Models;
using EPSMGest.Services.Requisicao;
using EPSMGest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EpsmGest.Controllers
{
    [Route("Requesicao/")]
    [Authorize]
    public class RequesicaoController : Controller
    {
        private IRequisicaoService RequisicoesService { get; set; }
        private IDepartamentoService DepartamentoService { get; set; }

        public RequesicaoController(IRequisicaoService _requisicoesService, IDepartamentoService _departamentoService)
        {
            RequisicoesService = _requisicoesService;
            DepartamentoService = _departamentoService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Index()
        {
            var model = RequisicoesService.GetRequesicoes();
            return View(model);
        }

        [HttpGet]
        [Route("Pessoais")]
        public IActionResult UserRequesicoes()
        {
            string userName = User.Identity.Name;
            var model = RequisicoesService.GetUserRequesicoes(userName);
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            ViewBag.Departamentos = DepartamentoService.GetDepartamentos();
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RequisicoesModel model)
        {
            RequisicoesService.CreateRequesicao(model);
            TempData["Sucess"] = "Requesição criada com sucesso!";
            if (User.IsInRole("Professor"))
                return RedirectToAction("Index","Home");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Details(string id)
        {
            var model = RequisicoesService.GetRequesicao(id);
            if (model == null)
                return NotFound();
            ViewBag.Departamentos = DepartamentoService.GetDepartamentos();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Edit(RequisicoesModel model)
        {
            RequisicoesService.EditRequesicao(model);
            TempData["Sucess"] = "Requesição editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Delete(string id)
        {
            bool flag = RequisicoesService.DeleteRequesicao(id);
            if (flag)
                TempData["Sucess"] = "Requesicao apagada com sucesso!";
            else
            {
                TempData["Error"] = "Requesição não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
                return RedirectToAction("Detalhes", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}
