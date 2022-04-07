using EPSMGest.Models;
using EpsmGest.Data;
using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Services.Purchase;
using EpsmGest.Services.Vehicle;

namespace EPSMGest.Services.Requisition
{
	public class RequisitionService : IRequisitionService
    {
        public readonly EpsmGestDbContext AppDb;
        public readonly IPurchaseService PurchaseService;
        public readonly IVehicleService VehicleService;

        public RequisitionService(EpsmGestDbContext Db, IPurchaseService _purchaseService, IVehicleService _vehicleService)
        {
            AppDb = Db;
            PurchaseService = _purchaseService;
            VehicleService = _vehicleService;
        }

        public List<RequisitionsViewModel> GetRequisitions()
        {
            return AppDb.Requisition.Join(AppDb.Department,
                req => Convert.ToInt32(req.DepartamentId),
                dep => dep.DepartamentId,
                (req, dep) => new RequisitionsViewModel
                {
                    RequisicaoId = req.RequisicaoId,
                    Applicant = req.Applicant,
                    Department = dep.Name,
                    Description = req.Description,
                    Date = req.Date,
                }).ToList();
        }

        public List<RequisitionsViewModel> GetUserRequisitions(string userName)
        {
            return AppDb.Requisition.Join(AppDb.Department,
                req => Convert.ToInt32(req.DepartamentId),
                dep => dep.DepartamentId,
                (req, dep) => new RequisitionsViewModel
                {
                    RequisicaoId = req.RequisicaoId,
                    Applicant = req.Applicant,
                    Department = dep.Name,
                    Description = req.Description,
                    Date = req.Date,
                }).Where(x => x.Applicant == userName).ToList();
        }

        public RequisitionsViewModel? GetRequisition(string Id)
        {
            return AppDb.Requisition.Join(AppDb.Department,
                req => Convert.ToInt32(req.DepartamentId),
                dep => dep.DepartamentId,
                (req, dep) => new RequisitionsViewModel
                {
                    RequisicaoId = req.RequisicaoId,
                    Applicant = req.Applicant,
                    Department = dep.Name,
                    Description = req.Description,
                    Date = req.Date,
                }).FirstOrDefault(x => x.RequisicaoId == Id.ToString());
        }

        public List<DropdownViewModel> GetRequisitionsIds()
        {
            return AppDb.Requisition.Select(x => new DropdownViewModel { Id = x.RequisicaoId, Name = x.RequisicaoId }).ToList();
        }

        public void CreateReqPurchase(CreateReqPurchaseViewModel model)
        {
            model.Requisition.Date = DateTime.Now;
            model.Requisition.RequisicaoId = UpdateReqId("ReqComp");
            AppDb.Requisition.Add(model.Requisition);
            AppDb.SaveChanges();
            PurchaseService.CreatePurchase(model);
        }

        public void CreateReqVehicle(CreateReqVehicleViewModel model)
		{
            model.Requisition.Date = DateTime.Now;
            model.Requisition.RequisicaoId = UpdateReqId("ReqVei");
            AppDb.Requisition.Add(model.Requisition);
            AppDb.SaveChanges();
            VehicleService.CreateRequestVehicle(model);
        }

        public void CreateReqSpace(CreateReqSpaceViewModel model)
        {
            model.Requisition.Date = DateTime.Now;
            model.Requisition.RequisicaoId = UpdateReqId("ReqEsp");
            AppDb.Requisition.Add(model.Requisition);
            AppDb.SaveChanges();
        }

        public void CreateReqIntervention(CreateReqInterventionViewModel model)
        {
            model.Requisition.Date = DateTime.Now;
            model.Requisition.RequisicaoId = UpdateReqId("ReqInt");
            AppDb.Requisition.Add(model.Requisition);
            AppDb.SaveChanges();
        }

        public void EditRequisition(RequisitionModel model)
        {
            AppDb.Requisition.Update(model);
            AppDb.SaveChanges();
        }

        public bool DeleteRequisition(string Id)
        {
            var requiscao = AppDb.Requisition.FirstOrDefault(x => x.RequisicaoId == Id);
            if (requiscao != null)
            {
                var compra = AppDb.Purchase.FirstOrDefault(x => x.RequisitionId == Id);
                if (compra == null)
                {
                    AppDb.Requisition.Remove(requiscao);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public string UpdateReqId(string code)
		{
            var lastReq = AppDb.Requisition.OrderByDescending(x => x.RequisicaoId).Where(x => x.RequisicaoId.Contains(code)).FirstOrDefault();
            if (lastReq != null)
            {
                var reqId = lastReq.RequisicaoId.Split('_');
                int nextint = Convert.ToInt32(reqId[2]) + 1;
                if (DateTime.Now.ToString("yyyy") == reqId[1])
                    return code + "_" + reqId[1] + "_" + nextint.ToString();
                else
                    return code + "_" + DateTime.Now.ToString("yyyy") + "_" + nextint.ToString();
            }
            return code + "_" + DateTime.Now.ToString("yyyy") + "_1";

        }
    }
}
