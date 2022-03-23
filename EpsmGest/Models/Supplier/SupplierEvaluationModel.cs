using EPSMGest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models
{
	public class SupplierEvaluationModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Supplier")]
		public int SupplierId { get; set; }

		[ForeignKey("Purchase")]
		public int PurchaseId { get; set; }

		public bool Param0 { get; set; } // Condições de Pagamento

		public bool Param1 { get; set; } // Cumprimentos dos requisitos

		public bool Param2 { get; set; } // Prazo de Entrega

		public bool Param3 { get; set; } // Preço

		public PurchaseModel Purchase { get; set; }

		public SupplierModel Supplier { get; set; }
	}
}
