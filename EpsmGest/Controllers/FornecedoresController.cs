using EPSMGest.Models;
using EPSMGest.Services.Fornecedores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EpsmGest.Controllers
{
    [Route("Fornecedores/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class FornecedoresController : Controller
    {
        private IFornecedoresService FornecedoresService { get; set; }

        public FornecedoresController(IFornecedoresService _FornecedoresService)
        {
            FornecedoresService = _FornecedoresService;
        }

        public IActionResult Index()
        {
            var model = FornecedoresService.GetFornecedores();
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FornecedorModel model)
        {
            FornecedoresService.CreateFornecedor(model);
            TempData["Sucess"] = "Fornecodor criado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        public IActionResult Details(string id)
        {
            int fornecedorId;
            if (!int.TryParse(id, out fornecedorId))
                return NotFound();
            var model = FornecedoresService.GetFornecedor(fornecedorId);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FornecedorModel model)
        {
            FornecedoresService.EditFornecedor(model);
            TempData["Sucess"] = "Fornecedor editado com sucesso!";
            return RedirectToAction("Detalhes", new { id = model.FornecedorId });
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(string id)
        {
            int fornecedorId;
            if (!int.TryParse(id, out fornecedorId))
                return NotFound();
            bool flag = FornecedoresService.DeleteFornecedor(fornecedorId);
            if (flag)
                TempData["Sucess"] = "Fornecedor apagada com sucesso!";
            else
                TempData["Error"] = "Fornecedor não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
            return RedirectToAction("Index");
        }
    }
}
