using EPSMGest.Models;
using EPSMGest.Services.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EpsmGest.Controllers
{
    [Route("Fornecedores/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class SupplierController : Controller
    {
        private ISupplierService SupplierService { get; set; }

        public SupplierController(ISupplierService _supplierService)
        {
            SupplierService = _supplierService;
        }

        public IActionResult Index()
        {
            var model = SupplierService.GetSuppliers();
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
        public IActionResult Create(SupplierModel model)
        {
            SupplierService.CreateSupplier(model);
            TempData["Success"] = "Fornecodor criado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        public IActionResult Details(string id)
        {
            int fornecedorId;
            if (!int.TryParse(id, out fornecedorId))
                return NotFound();
            var model = SupplierService.GetSupplier(fornecedorId);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SupplierModel model)
        {
            SupplierService.EditSupplier(model);
            TempData["Success"] = "Fornecedor editado com sucesso!";
            return RedirectToAction("Detalhes", new { id = model.SupplierId });
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(string id)
        {
            int fornecedorId;
            if (!int.TryParse(id, out fornecedorId))
                return NotFound();
            bool flag = SupplierService.DeleteSupplier(fornecedorId);
            if (flag)
                TempData["Success"] = "Fornecedor apagada com sucesso!";
            else
                TempData["Error"] = "Fornecedor não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
            return RedirectToAction("Index");
        }
    }
}
