using Microsoft.EntityFrameworkCore;

namespace EpsmGest.ViewModel.Purchase
{
	[Keyless]
	public class NecessidadeViewModel
	{
		public int Id { get; set; }

		public string PurchaseId { get; set; }

		public string RequisitionId { get; set; }

		public int Stage { get; set; }

		public decimal TotalPrice { get; set; }

		public string Applicant { get; set; }

		public DateTime Date { get; set; }

		public string Department { get; set; }

		public int DepartamentId { get; set; }

		public string Description { get; set; }		

		public List<SimplePurchaseItemViewModel>? PurchaseItem { get; set; }
	}
}
