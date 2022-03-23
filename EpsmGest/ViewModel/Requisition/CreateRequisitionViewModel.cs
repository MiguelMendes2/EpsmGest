using EPSMGest.Models;

namespace EpsmGest.ViewModel.Requisition
{
	public class CreateRequisitionViewModel
	{
		public RequisitionModel Requisition { get; set; }

		public List<PurchaseItemModel> PurchaseItem { get; set; }
	}
}
