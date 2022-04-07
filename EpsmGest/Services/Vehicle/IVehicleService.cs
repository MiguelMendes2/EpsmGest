using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Models.Vehicle;

namespace EpsmGest.Services.Vehicle
{
	public interface IVehicleService
	{
		// Request Vehicles

		public RequestVehicleModel? GetRequestVehicle(int id);

		public List<RequestVehicleModel> GetRequestVehicles();

		public List<RequestVehicleModel> GetHistoricRequestVehicles();

		public void CreateRequestVehicle(CreateReqVehicleViewModel model);

		public bool EditRequestVehicle(RequestVehicleModel model);

		public bool ChangeAproval(int Id);

		public bool RemoveRequestVehicle(int id);

		// Vehicles 

		public IEnumerable<VehicleModel> GetVehicles();

		public VehicleModel? GetVehicle(int id);

		public List<DropdownViewModel> GetVehiclesIds();

		public bool CreateVehicle(VehicleModel model);

		public bool EditVehicle(VehicleModel model);

		public bool RemoveVehicle(int id);
	}
}
