using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Models;

namespace EPSMGest.Services.Requisition
{
    public interface IRequisitionService
    {
        public List<RequisitionsViewModel> GetRequisitions();

        public List<RequisitionsViewModel> GetUserRequisitions(string userName);

        public RequisitionsViewModel GetRequisition(string Id);

        public List<DropdownViewModel> GetRequisitionsIds();

        public void CreateReqPurchase(CreateReqPurchaseViewModel model);

        public void CreateReqVehicle(CreateReqVehicleViewModel model);

        public void CreateReqSpace(CreateReqSpaceViewModel model);

        public void CreateReqIntervention(CreateReqInterventionViewModel model);

        public void EditRequisition(RequisitionModel model);

        public bool DeleteRequisition(string Id);
    }
}
