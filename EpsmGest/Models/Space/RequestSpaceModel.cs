using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models.Space
{
	public class RequestSpaceModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public bool Approval { get; set; }

		[ForeignKey("Requisition")]
		public string RequisitionId { get; set; }
		public RequisitionModel Requisition { get; set; }

		[ForeignKey("Space"), Required]
		public int SpaceId { get; set; }
		public SpaceModel Space { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

		[Required]
		public string Activity { get; set; }
	}
}
