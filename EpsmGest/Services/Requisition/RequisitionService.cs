using EPSMGest.Models;
using EpsmGest.Data;
using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Services.Purchase;

namespace EPSMGest.Services.Requisition
{
    public class RequisitionService : IRequisitionService
    {
        public readonly EpsmGestDbContext AppDb;
        public readonly IPurchaseService PurchaseService;

        public RequisitionService(EpsmGestDbContext Db, IPurchaseService _purchaseService)
        {
            AppDb = Db;
            PurchaseService = _purchaseService;
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

        public async Task CreateRequisition(CreateRequisitionViewModel model)
        {
            var lastReq = AppDb.Requisition.OrderByDescending(x => x.RequisicaoId).FirstOrDefault();
            if (lastReq != null)
            {
                var reqId = lastReq.RequisicaoId.Split('_');
                int nextint = Convert.ToInt32(reqId[2]) + 1;
                if (DateTime.Now.ToString("yyyy") == reqId[1])
                {
                    model.Requisition.RequisicaoId = "Req_" + reqId[1] + "_" + nextint.ToString();
                }
                else
                {
                    model.Requisition.RequisicaoId = "Req_" + DateTime.Now.ToString("yyyy") + "_" + nextint.ToString();
                }
            }
            else
            {
                model.Requisition.RequisicaoId = "Req_" + DateTime.Now.ToString("yyyy") + "_1";
            }
                AppDb.Requisition.Add(model.Requisition);
            AppDb.SaveChanges();
            PurchaseService.CreatePurchase(model);
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


    }
}
