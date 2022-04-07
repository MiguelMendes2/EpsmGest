using EPSMGest.Models;
using EPSMGest.Services.Requisition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EpsmGest.ViewModel.Requisition;
using EpsmGest.Services.Department;
using EpsmGest.Services.Vehicle;

namespace EpsmGest.Controllers
{
	[Authorize]
    [Route("Requesicao/"), Route("Requisition/")]
    public class RequisitionController : Controller
    {
        private IRequisitionService RequisitionService { get; set; }
        private IDepartmentService DepartmentService { get; set; }
        private IVehicleService VehicleService { get; set; }

        public RequisitionController(IRequisitionService _requisitionService, IDepartmentService _departmentService, IVehicleService _vehicleService)
        {
            RequisitionService = _requisitionService;
            DepartmentService = _departmentService;
            VehicleService = _vehicleService;
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

        // --- REQUEST PURCHASE ---
        [HttpGet]
        [Route("CriarCompra"), Route("CreatePurchase")]
        public IActionResult CreatePurchase()
        {
            ViewBag.Departamentos = DepartmentService.GetDepartmentIds();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CriarCompra"), Route("CreatePurchase")]
        public IActionResult CreatePurchase(CreateReqPurchaseViewModel model)
        {
            model.Requisition.Applicant = User.Identity.Name;
            RequisitionService.CreateReqPurchase(model);
            TempData["Success"] = "Requesição de compra criada com sucesso!";
            return RedirectToAction("Pessoais");
        }

        // --- REQUEST INTERVENTION ---

        [HttpGet]
        [Route("CriarIntervencao"), Route("CreateIntervention")]
        public IActionResult CreateIntervention()
        {
            ViewBag.Departamentos = DepartmentService.GetDepartmentIds();
            ViewBag.Vehicles = VehicleService.GetVehiclesIds();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CriarIntervencao"), Route("CreateIntervention")]
        public IActionResult CreateIntervention(CreateReqInterventionViewModel model)
        {
            
            TempData["Success"] = "Pedido de intervenção criado com sucesso!";
            return RedirectToAction("Pessoais");
        }

        // --- REQUEST VEHICLE ---

        [HttpGet]
        [Route("CriarVeiculo"), Route("CreateVehicle")]
        public IActionResult CreateVehicle()
        {
            ViewBag.Departamentos = DepartmentService.GetDepartmentIds();
            ViewBag.Vehicles = VehicleService.GetVehiclesIds();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CriarVeiculo"), Route("CreateVehicle")]
        public IActionResult CreateVehicle(CreateReqVehicleViewModel model)
        {
            model.Requisition.Applicant = User.Identity.Name;
            RequisitionService.CreateReqVehicle(model);
            TempData["Success"] = "Requesição de viatura criado com sucesso!";
            return RedirectToAction("Pessoais");
        }

        // --- REQUEST SPACE ---

        [HttpGet]
        [Route("CriarEspaco"), Route("CreateSpace")]
        public IActionResult CreateSpace()
        {
            ViewBag.Departamentos = DepartmentService.GetDepartmentIds();
            ViewBag.Vehicles = VehicleService.GetVehiclesIds();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CriarEspaco"), Route("CreateSpace")]
        public IActionResult CreateSpace(CreateReqSpaceViewModel model)
        {

            TempData["Success"] = "Requesição de espaço criado com sucesso!";
            return RedirectToAction("Pessoais");
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
