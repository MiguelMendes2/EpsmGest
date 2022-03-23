using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models.Vehicles
{
	public class VehicleModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string License { get; set; } // Matricula do carro

		public ICollection<RequestVehicleModel>? RequestVehicles { get; set; }
	}
}
