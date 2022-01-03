using EPSMGest.Models;
using EPSMGest.Services.Compras;
using EPSMGest.Services.Faturas;
using EPSMGest.Services.Fornecedores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
    [Route("Faturas/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class FaturasController : Controller
    {
        private IComprasService ComprasService { get; set; }
        private IFornecedoresService FornecedoresService { get; set; }
        private IFaturasService FaturasService { get; set; }

        public FaturasController(IComprasService _ComprasSerivice, IFaturasService _faturasService, IFornecedoresService _fornecedoresService)
        {
            ComprasService = _ComprasSerivice;
            FaturasService = _faturasService;
            FornecedoresService = _fornecedoresService;
        }

        public IActionResult Index()
        {
            var model = FaturasService.GetFaturas();
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            ViewBag.Compras = ComprasService.GetComprasIds();
            ViewBag.Fornecedor = FornecedoresService.GetFornecedoresIds();
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FaturasModel model)
        {
            if (ModelState.IsValid)
            {
                FaturasService.CreateFatura(model);
                TempData["Sucess"] = "Fatura criada com sucesso!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        public IActionResult Details(string id)
        {
            int faturaId;
            if (!int.TryParse(id, out faturaId))
                return NotFound();
            var model = FaturasService.GetFatura(faturaId);
            if (model == null)
                return NotFound();
            ViewBag.Compras = ComprasService.GetComprasIds();
            ViewBag.Fornecedor = FornecedoresService.GetFornecedoresIds();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FaturasModel model)
        {
            FaturasService.EditFatura(model);
            TempData["Sucess"] = "Fatura editado com sucesso!";
            return RedirectToAction("Detalhes", new { Id = model.FaturasId });
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(int id)
        {
            FaturasService.DeleteFatura(id);
            TempData["Sucess"] = "Fatura apagada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
