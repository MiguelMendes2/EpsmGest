using EPSMGest.Models.Purchase;

namespace EpsmGest.ViewModel.Purchase
{
	public class ConsultaMercadoViewModel
	{
		public int Id { get; set; }

		public int Stage { get; set; }

		public string Category { get; set; }

		public string Description { get; set; }

		public decimal TotalPrice { get; set; }

		public List<PurchaseItemModel> PurchaseItem { get; set; }
	}
}
