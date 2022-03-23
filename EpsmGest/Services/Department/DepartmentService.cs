using EpsmGest.Data;
using EPSMGest.Models;

namespace EPSMGest.Services
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

        public void EditDepartment(DepartmentModel model)
        {
            AppDb.Department.Update(model);
            AppDb.SaveChanges();
        }
    }
}
