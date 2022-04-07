using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EpsmGest.Services.Department
{
	public interface IDepartmentService
	{
		public List<DepartmentModel> GetDepartments();

		public DepartmentModel GetDepartment(int id);

		public List<DropdownViewModel> GetDepartmentIds();

		public void CreateDepartment(DepartmentModel model);

		public bool DeleteDepartment(int id);

		public bool EditDepartment(DepartmentModel model);
	}
}
