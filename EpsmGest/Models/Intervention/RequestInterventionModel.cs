using EpsmGest.Models.Intervention;
using EpsmGest.Models.Stocks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models
{
	public class RequestInterventionModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public bool Completed { get; set; }

		[Required, ForeignKey("Equipament")]
		public string EquipamentId { get; set; }
		public EquipmentModel Equipament { get; set; }

		[Required]
		public string Description { get; set; }		
	}
}
