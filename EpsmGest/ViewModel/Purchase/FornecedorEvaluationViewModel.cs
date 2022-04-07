using EPSMGest.Models;

namespace EpsmGest.ViewModel.Purchase
{
	public class FornecedorEvaluationViewModel
	{
		public int Id { get; set; }

		public int Stage { get; set; }

		public string Category { get; set; }

		public string Description { get; set; }

		public decimal TotalPrice { get; set; }

		public List<Evaluation> Evaluations { get; set; }
	}

	public class Evaluation
	{
		public int EvalutionId { get; set; }

		public int SupplierId { get; set; }

		public string SupplierName { get; set; }

		public bool Param0 { get; set; } // Condições de Pagamento

		public bool Param1 { get; set; } // Cumprimentos dos requisitos

		public bool Param2 { get; set; } // Prazo de Entrega

		public bool Param3 { get; set; } // Preço

		public InvoiceModel? Invoice { get; set; }
	}
}
