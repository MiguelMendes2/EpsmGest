using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models.Spaces
{
	public class RequestSpaceModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public bool Approval { get; set; }

		[ForeignKey("Space"), Required]
		public string SpaceId { get; set; }
		public SpaceModel Space { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

		[Required]
		public string Activity { get; set; }

		[Required]
		public string Description { get; set; }


	}
}
