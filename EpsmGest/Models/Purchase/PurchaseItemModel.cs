using EPSMGest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.ViewModel
{
	public class PurchaseItemModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Purchase")]
		[Required(ErrorMessage = "Este campo é obrigatório")]
		public int PurchaseId { get; set; }
		public PurchaseModel Purchase { get; set; }

		[ForeignKey("Supplier")]
		public int? SupplierId { get; set; }
		public SupplierModel? Supplier { get; set; }

		[Required(ErrorMessage = "Este campo é obrigatório")]
		public string Item { get; set; }

		[Required(ErrorMessage = "Este campo é obrigatório")]
		[Range(1,10000000,ErrorMessage = "O valor minimo de quantidade é 1!")]
		public int Quantity { get; set; }


		[Required(ErrorMessage = "Este campo é obrigatório")]
		[Range(0.00, Double.MaxValue, ErrorMessage = "O valor minimo de quantidade é 0!")]
		public decimal Price { get; set; }

		[Display(Name = "Parecer 1")]
		public bool Aprovation1 { get; set; }

		[Display(Name = "Parecer 2")]
		public bool Aprovation2 { get; set; }
	}
}
