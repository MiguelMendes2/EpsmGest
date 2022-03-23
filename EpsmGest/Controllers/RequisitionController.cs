using EPSMGest.Models;
using EPSMGest.Services.Requisition;
using EPSMGest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EpsmGest.ViewModel.Requisition;

namespace EpsmGest.Controllers
{
    [Route("Requesicao/")]
    [Authorize]
    public class RequisitionController : Controller
    {
        private IRequisitionService RequisitionService { get; set; }
        private IDepartmentService DepartmentService { get; set; }

        public RequisitionController(IRequisitionService _requisitionService, IDepartmentService _departmentService)
        {
            RequisitionService = _requisitionService;
            DepartmentService = _departmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Index()
        {
            var model = RequisitionService.GetRequisitions();
            return View(model);
        }

        [HttpGet]
        [Route("Pessoais")]
        public IActionResult UserRequesicoes()
        {
            string userName = User.Identity.Name;
            var model = RequisitionService.GetUserRequisitions(userName);
            return View(model);
        }

        [HttpGet]
        [Route("Criar")]
        public IActionResult Create()
        {
            ViewBag.Departamentos = DepartmentService.GetDepartments();
            return View();
        }

        [HttpPost]
        [Route("Criar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRequisitionViewModel model)
        {
            model.Requisition.Applicant = User.Identity.Name;
            await RequisitionService.CreateRequisition(model);
            TempData["Success"] = "Requesição criada com sucesso!";
            if (User.IsInRole("Professor"))
                return RedirectToAction("Index","Home");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Detalhes/{id}")]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Details(string id)
        {
            var model = RequisitionService.GetRequisition(id);
            if (model == null)
                return NotFound();
            ViewBag.Departamentos = DepartmentService.GetDepartments();
            return View(model);
        }

        [HttpPost]
        [Route("Editar")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Edit(RequisitionModel model)
        {
            RequisitionService.EditRequisition(model);
            TempData["Success"] = "Requesição editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Apagar/{id}")]
        [Authorize(Roles = "Administrator,Funcionário DGAR")]
        public IActionResult Delete(string id)
        {
            bool flag = RequisitionService.DeleteRequisition(id);
            if (flag)
                TempData["Success"] = "Requesicao apagada com sucesso!";
            else
            {
                TempData["Error"] = "Requesição não foi apagado, verifique se o mesmo não está a ser usado em outro registo!";
                return RedirectToAction("Detalhes", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}
