using EPSMGest.Models;

namespace EPSMGest.Services
{
    public interface IDepartmentService
    {
        public List<DepartmentModel> GetDepartments();

        public DepartmentModel GetDepartment(int id);

        public void CreateDepartment(DepartmentModel model);

        public bool DeleteDepartment(int id);

        public void EditDepartment(DepartmentModel model);
    }
}
