using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models.Spaces
{
	public class SpaceModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<RequestSpaceModel>? RequestSpaces { get; set; }
	}
}
