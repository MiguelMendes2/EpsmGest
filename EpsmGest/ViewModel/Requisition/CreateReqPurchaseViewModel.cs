using EPSMGest.Models;
using EPSMGest.Models.Purchase;

namespace EpsmGest.ViewModel.Requisition
{
	public class CreateReqPurchaseViewModel
	{
		public RequisitionModel Requisition { get; set; }

		public List<PurchaseItemModel> PurchaseItem { get; set; }
	}
}
