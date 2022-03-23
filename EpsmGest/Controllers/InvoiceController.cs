using EPSMGest.Models;
using EPSMGest.Services.Purchase;
using EPSMGest.Services.Invoice;
using EPSMGest.Services.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpsmGest.Controllers
{
    [Route("Faturas/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class InvoiceController : Controller
    {
        private IPurchaseService PurchaseService { get; set; }
        private ISupplierService SupplierService { get; set; }
        private IInvoiceService InvoiceService { get; set; }

        public InvoiceController(IPurchaseService _purchaseService, IInvoiceService _invoiceService, ISupplierService _supplierService)
        {
            PurchaseService = _purchaseService;
            InvoiceService = _invoiceService;
            SupplierService = _supplierService;
        }

        public IActionResult Index()
        {
            var model = InvoiceService.GetInvoices();
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            ViewBag.Compras = PurchaseService.GetPurchasesIds();
            ViewBag.Fornecedor = SupplierService.GetSuppliersIds();
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InvoiceModel model)
        {
            if (ModelState.IsValid)
            {
                InvoiceService.CreateInvoice(model);
                TempData["Success"] = "Fatura criada com sucesso!";
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
            var model = InvoiceService.GetInvoice(faturaId);
            if (model == null)
                return NotFound();
            ViewBag.Compras = PurchaseService.GetPurchasesIds();
            ViewBag.Fornecedor = SupplierService.GetSuppliersIds();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InvoiceModel model)
        {
            InvoiceService.EditInvoice(model);
            TempData["Success"] = "Fatura editado com sucesso!";
            return RedirectToAction("Detalhes", new { Id = model.InvoiceId });
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(int id)
        {
            InvoiceService.DeleteInvoice(id);
            TempData["Success"] = "Fatura apagada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
