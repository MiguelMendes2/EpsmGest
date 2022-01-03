using EPSMGest.Models;

namespace EPSMGest.Services
{
    public interface IDepartamentoService
    {
        public List<DepartamentoModel> GetDepartamentos();

        public DepartamentoModel GetDepartamento(int id);

        public void CreateDepartamento(DepartamentoModel model);

        public bool DeleteDepartamento(int id);

        public void EditDepartamento(DepartamentoModel model);
    }
}
