using EPSMGest.Models.Stocks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models.Intervention
{
	public class RequestInterventionModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Requisition")]
		public string RequisitionId { get; set; }
		public RequisitionModel Requisition { get; set; }

		public string InterventionId { get; set; }

		public int Stage { get; set; }

		[Required, ForeignKey("Equipament")]
		public int EquipamentId { get; set; }
		public EquipmentModel Equipament { get; set; }
	}

	/* Stage:
		0 -> Pendente
		1 -> Em analise
		2 -> Concluido
		
	 */
}
