using EpsmGest.ViewModel.Purchase;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Models;
using EPSMGest.Services.Purchase;
using EPSMGest.Services.Supplier;
using EPSMGest.Services.Requisition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EPSMGest.Models.Purchase;
using EpsmGest.Services.Department;

namespace EpsmGest.Controllers
{
	[Route("Compras/")]
    [Authorize(Roles = "Administrator,Funcionário DGAR")]
    public class PurchaseController : Controller
    {
        private IPurchaseService PurchaseService { get; set; }
        private IRequisitionService RequisitionService { get; set; }
        private IDepartmentService DepartmentService { get; set; }
        private ISupplierService SupplierService { get; set; }

        public PurchaseController(IPurchaseService _purchaseService, IRequisitionService _requisitionService, IDepartmentService _departmentService, ISupplierService _supplierService)
        {
            PurchaseService = _purchaseService;
            RequisitionService = _requisitionService;
            DepartmentService = _departmentService;
            SupplierService = _supplierService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = PurchaseService.GetPurchases();
            return View(model);
        }

        /* -------
          NECESSIDADE
           ------- */

        [HttpGet]
        [Route("Necessidade/{id}")]
        public IActionResult Necessidade(string id)
		{
            ViewBag.Id = id;
            if (Int32.TryParse(id, out int result))
			{
                var model = PurchaseService.GetNecessidade(result);
                if(model != null)
				{
                    ViewBag.Stage = model.Stage;
                    ViewBag.TotalPrice = model.TotalPrice;
                    ViewBag.Departamentos = DepartmentService.GetDepartmentIds();
                    ViewBag.TotalPrice = model.TotalPrice;
                    return View(model);
                }
            }
            return NotFound();
		}

        [HttpPost]
        [Route("EditNecessidade")]
        public IActionResult EditNecessidade(NecessidadeViewModel model)
		{
            if(model.PurchaseId != null && model.RequisitionId != null)
			{
                var result = PurchaseService.EditNecessidade(model);
                if (result)
                    TempData["Success"] = "Necessidade editada";
                else
                    TempData["Error"] = "Não foi possivel editar! Tente novamente mais tade ";
			}
            return RedirectToAction("Necessidade", new { Id = model.Id });
		}

        /* -------
          CONSULTA DE MERCADO
           ------- */

        [Route("ConsultaMercado/{id}")]
        public IActionResult ConsultaMercado(string id)
		{
            if (Int32.TryParse(id, out int result))
			{
                ViewBag.Id = id;
                var model = PurchaseService.GetConsultaMercado(result);
                if (model == null)
                    return NotFound();
                ViewBag.Fornecedores = SupplierService.GetSuppliersIds();
                ViewBag.Stage = model.Stage;
                ViewBag.TotalPrice = model.TotalPrice;
                return View(model);
            }
            return NotFound();
        }
                
        [HttpPost]
        [Route("EditConsultaMercado")]
        public IActionResult EditConsultaMercado(ConsultaMercadoViewModel model)
        {
            if(model.Id != 0 && model.Stage == 0)
			{
                var result = PurchaseService.EditConsultaMercado(model);
                if (result)
                    TempData["Success"] = "Consulta de Mercado editada";
                else
                    TempData["Error"] = "Não foi possivel editar! Tente novamente mais tade ";
            }
            return RedirectToAction("ConsultaMercado", new { id = model.Id });
        }

        [Route("CloseConsultaMercado/{id}")]
        public IActionResult CloseConsultaMercado(string id)
		{
            if (Int32.TryParse(id, out int result))
            {
                var flag = PurchaseService.CloseConsultaMercado(result);
                if (flag)
                    TempData["Success"] = "Consulta de mercado fechada";
                else
                    TempData["Error"] = "Não é possivel fechar a consulta de mercado verifique se preencheu todos os campos";
                return RedirectToAction("ConsultaMercado", new { id = id });
            }
            TempData["Error"] = "Não foi possivel fechar a consulta de mercado";
            return RedirectToAction("ConsultaMercado", new { id = id });
        }

        /* -------
          PARECER 1
           ------- */

        [Route("Parecer1/{id}")]
        public IActionResult Parecer1(string id)
        {
            if (Int32.TryParse(id, out int result))
            {
                ViewBag.Id = id;
                var model = PurchaseService.GetConsultaMercado(result);
                if (model == null)
                    return NotFound();
                ViewBag.Stage = model.Stage;
                ViewBag.TotalPrice = model.TotalPrice;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("SetParecer1")]
        public IActionResult SetParecer1(ConsultaMercadoViewModel model)
        {
            if (model.Id != 0 && model.Stage == 1)
            {
                var result = PurchaseService.SetParecer1(model);
                if (result)
                    TempData["Success"] = "Parecer 1 Adicionado!";
                else
                    TempData["Error"] = "Não foi possivel adicionar o parecer 1, tente novamente mais tarde!";
            }
            return RedirectToAction("Parecer1", new { id = model.Id });
        }

        [HttpGet]
        [Route("CloseParecer1/{id}")]
        public IActionResult CloseParecer1(string id)
		{
            if (Int32.TryParse(id, out int result))
            {
                var flag = PurchaseService.CloseParecer1(result);
                if (flag)
                    TempData["Success"] = "Parecer 1 Fechado";
                else
                    TempData["Error"] = "Não foi possivel fechar o parecer 1, tente novamente mais tarde!";
                return RedirectToAction("Parecer1", new { id = id });
            }
            TempData["Error"] = "Não foi possivel fechar a consulta de mercado";
            return RedirectToAction("Parecer1", new { id = id });
        }

        /* -------
          PARECER 2
           ------- */

        [Route("Parecer2/{id}")]
        public IActionResult Parecer2(string id)
        {
            if (Int32.TryParse(id, out int result))
            {
                ViewBag.Id = id;
                var model = PurchaseService.GetParecer2(result);
                if (model == null)
                    return NotFound();
                ViewBag.Stage = model.Stage;
                ViewBag.TotalPrice = model.TotalPrice;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("SetParecer2")]
        public IActionResult SetParecer2(ConsultaMercadoViewModel model)
        {
            if (model.Id != 0 && model.Stage == 2)
            {
                var result = PurchaseService.SetParecer2(model);
                if (result)
                    TempData["Success"] = "Parecer 2 Adicionado!";
                else
                    TempData["Error"] = "Não foi possivel adicionar o parecer 2, tente novamente mais tarde!";
            }
            return RedirectToAction("Parecer2", new { id = model.Id });
        }

        [HttpGet]
        [Route("CloseParecer2/{id}")]
        public IActionResult CloseParecer2(string id)
        {
            if (Int32.TryParse(id, out int result))
            {
                var flag = PurchaseService.CloseParecer2(result);
                if (flag)
                    TempData["Success"] = "Parecer 2 Fechado";
                else
                    TempData["Error"] = "Não foi possivel fechar o parecer 2, tente novamente mais tarde!";
                return RedirectToAction("Parecer2", new { id = id });
            }
            TempData["Error"] = "Não foi possivel fechar a consulta de mercado";
            return RedirectToAction("Parecer2", new { id = id });
        }

        /* -------
          AVALIAÇÃO
           ------- */
        [HttpGet]
        [Route("Avaliation")]
        public IActionResult Avaliation(string id)
		{
            if (Int32.TryParse(id, out int result))
            {
                ViewBag.Id = id;
                var model = PurchaseService.GetFornecedorEvaluation(result);
                if (model == null)
                    return NotFound();
                ViewBag.Stage = model.Stage;
                ViewBag.TotalPrice = model.TotalPrice;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("SetAvaliation")]
        public IActionResult SetAvaliation(FornecedorEvaluationViewModel model)
        {
            if (model.Id != 0 && model.Stage == 0)
            {
                var result = PurchaseService.SetFornecedorEvaluation(model);
                if (result)
                    TempData["Success"] = "Avaliação adicionada!";
                else
                    TempData["Error"] = "Não foi possivel adicionar avaliação, tente novamente mais tarde!";
            }
            return RedirectToAction("Avaliation", new { id = model.Id });
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult Edit(PurchaseModel model)
        {
            ViewBag.RequisicaoIds = RequisitionService.GetRequisitionsIds();
            PurchaseService.EditPurchase(model);
            return RedirectToAction("Detalhes", new { Id = model.Id });
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        public IActionResult Delete(string id)
        {
            if (!int.TryParse(id, out int purchaseId))
                return NotFound();
            bool flag = PurchaseService.DeletePurchase(purchaseId);
            if (flag)
                TempData["Success"] = "Compra apagada com sucesso!";
            else
                TempData["Error"] = "Esta compra possui uma fatura, apage a fatura para poder remover a compra";
            return RedirectToAction("Index");
        }
    }
}
