using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpsmGest.Models.Vehicles
{
	public class RequestVehicleModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public bool Approval { get; set; }

		[Required]
		public string Service { get; set; }

		[Required]
		public DateTime Departure { get; set; }

		[Required]
		public DateTime Arrival { get; set; }

		[Required]
		public string Origin { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Driver { get; set; }

		[Range(1,9, ErrorMessage = "Introduza um valor maior que 1!")]
		public int Occupants { get; set; }

		[Required,ForeignKey("Vehicle")]
		public int VehicleId { get; set; }
		public VehicleModel Vehicle { get; set; }

		// -- Campos Opcionais --

		public int KmsOnDeparture { get; set; }

		public int KmsOnArrival { get; set; }

		public double Gas { get; set; }
	}
}
