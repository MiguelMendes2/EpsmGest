using EPSMGest.Models;
using EPSMGest.Services.Compras;
using EPSMGest.Services.Requisicao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
    [Route("Compras/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class ComprasController : Controller
    {
        private IComprasService ComprasService { get; set; }
        private IRequisicaoService RequisicoesService { get; set; }

        public ComprasController(IComprasService _ComprasSerivice, IRequisicaoService _requisicoesService)
        {
            ComprasService = _ComprasSerivice;
            RequisicoesService = _requisicoesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = ComprasService.GetCompras();
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            ViewBag.Requesicoes = RequisicoesService.GetRequesicaoIds();
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ComprasModel model)
        {
            ComprasService.CreateCompra(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        public IActionResult Details(string id)
        {
            int compraId;
            if (!int.TryParse(id, out compraId))
                return NotFound();
            var model = ComprasService.GetCompra(compraId);
            if (model == null)
                return NotFound();
            ViewBag.Requesicoes = RequisicoesService.GetRequesicaoIds();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(ComprasModel model)
        {
            ViewBag.RequisicaoIds = RequisicoesService.GetRequesicaoIds();
            ComprasService.EditCompra(model);
            return RedirectToAction("Detalhes", new { Id = model.Id });
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(string id)
        {
            int compraId;
            if (!int.TryParse(id, out compraId))
                return NotFound();
            bool flag = ComprasService.DeleteCompra(compraId);
            if (flag)
                TempData["Sucess"] = "Compra apagada com sucesso!";
            else
                TempData["Error"] = "Algo correu mal, compra não foi apagada!";
            return RedirectToAction("Index");
        }
    }
}
