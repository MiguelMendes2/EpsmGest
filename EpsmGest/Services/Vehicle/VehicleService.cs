using EpsmGest.Data;
using EPSMGest.Models.Vehicle;
using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Requisition;
using Microsoft.EntityFrameworkCore;

namespace EpsmGest.Services.Vehicle
{
	public class VehicleService : IVehicleService
	{
		private readonly EpsmGestDbContext AppDb;

		public VehicleService(EpsmGestDbContext _appDb)
		{
			AppDb = _appDb;
		}

		public RequestVehicleModel? GetRequestVehicle(int id)
		{
			return AppDb.RequestVehicle.Include(x => x.Requisition).FirstOrDefault(x => x.Id == id);
		}

		public List<RequestVehicleModel> GetRequestVehicles()
		{
			return AppDb.RequestVehicle.Include(x => x.Requisition).Where(x => x.Approval == false).ToList();
		}

		public List<RequestVehicleModel> GetHistoricRequestVehicles()
		{
			return AppDb.RequestVehicle.Where(x => x.Departure < DateTime.Now).ToList();
		}

		public void CreateRequestVehicle(CreateReqVehicleViewModel model)
		{
			model.ReqVehicle.RequisitionId = model.Requisition.RequisicaoId;
			AppDb.RequestVehicle.Add(model.ReqVehicle);
			AppDb.SaveChanges();
		}

		public bool EditRequestVehicle(RequestVehicleModel model)
		{
			var request = AppDb.RequestVehicle.Include(x => x.Requisition).FirstOrDefault(x => x.Id == model.Id && x.Approval == false);
			if (request == null)
				return false;
			AppDb.RequestVehicle.Update(model);
			AppDb.SaveChanges();
			return true;
		}

		public bool ChangeAproval(int Id)
		{
			var request = AppDb.RequestVehicle.FirstOrDefault(x => x.Id == Id);
			if (request == null)
				return false;
			if (request.Approval == false)
				request.Approval = true;
			else
				request.Approval = false;
			AppDb.RequestVehicle.Update(request);
			AppDb.SaveChanges();
			return true;
		}

		public bool RemoveRequestVehicle(int id)
		{
			var request = AppDb.RequestVehicle.FirstOrDefault(x => x.Id == id);
			if (request == null)
				return false;
			AppDb.Remove(request);
			AppDb.SaveChanges();
			return true;
		}

		/*
		 --- Vehicles ---
		*/

		public IEnumerable<VehicleModel> GetVehicles()
		{
			return AppDb.Vehicle;
		}

		public VehicleModel? GetVehicle(int id)
		{
			return AppDb.Vehicle.FirstOrDefault(x => x.Id == id);
		}

		public List<DropdownViewModel> GetVehiclesIds()
		{
			return AppDb.Vehicle.Select(x => new DropdownViewModel { Id = x.Id.ToString(), Name = x.License + " " + x.Name }).ToList();
		}

		public bool CreateVehicle(VehicleModel model)
		{
			if (AppDb.Vehicle.Where(x => x.License == model.License).Any())
				return false;
			AppDb.Vehicle.Add(model);
			AppDb.SaveChanges();
			return true;
		}

		public bool EditVehicle(VehicleModel model)
		{
			var vehicle = AppDb.Vehicle.FirstOrDefault(x => x.Id == model.Id);
			if (vehicle == null)
				return false;
			vehicle.Name = model.Name;
			vehicle.License = model.License;
			AppDb.Update(vehicle);
			AppDb.SaveChanges();
			return true;
		}

		public bool RemoveVehicle(int id)
		{
			var vehicle = AppDb.Vehicle.FirstOrDefault(x => x.Id == id);
			if (vehicle == null)
				return false;
			AppDb.Remove(vehicle);
			AppDb.SaveChanges();
			return true;
		}
	}
}
