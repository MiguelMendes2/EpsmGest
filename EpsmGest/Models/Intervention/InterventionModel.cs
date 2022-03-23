using EpsmGest.Models.Stocks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models.Intervention
{
	public class InterventionModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, ForeignKey("Equipament")]
		public int EquipamentId { get; set; }
		public EquipmentModel Equipament { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public string Description { get; set; }
	}
}
