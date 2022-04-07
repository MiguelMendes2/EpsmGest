using EpsmGest.Data;
using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EpsmGest.Services.Department
{
	public class DepartmentService : IDepartmentService
	{
		public readonly EpsmGestDbContext AppDb;

		public DepartmentService(EpsmGestDbContext Db)
		{
			AppDb = Db;
		}

		public List<DepartmentModel> GetDepartments()
		{
			return AppDb.Department.ToList();
		}

		public DepartmentModel? GetDepartment(int id)
		{
			return AppDb.Department.FirstOrDefault(x => x.DepartamentId == id);
		}

		public List<DropdownViewModel> GetDepartmentIds()
		{
			return AppDb.Department.Select(x => new DropdownViewModel { Id = x.DepartamentId.ToString(), Name = x.Name }).ToList();
		}

		public void CreateDepartment(DepartmentModel model)
		{
			AppDb.Department.Add(model);
			AppDb.SaveChanges();
		}

		public bool DeleteDepartment(int id)
		{
			var departamento = AppDb.Department.FirstOrDefault(x => x.DepartamentId == id);
			if (departamento != null)
			{
				var requisicao = AppDb.Requisition.FirstOrDefault(x => x.DepartamentId == id);
				if (requisicao == null)
				{
					AppDb.Department.Remove(departamento);
					AppDb.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool EditDepartment(DepartmentModel model)
		{
			var department = AppDb.Department.FirstOrDefault(x => x.DepartamentId == model.DepartamentId);
			if (department == null)
				return false;
			department.Name = model.Name;
			AppDb.SaveChanges();
			return true;
		}
	}
}
