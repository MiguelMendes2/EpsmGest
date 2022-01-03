using EpsmGest.Data;
using EPSMGest.Models;

namespace EPSMGest.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        public readonly EpsmGestDbContext AppDb;

        public DepartamentoService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<DepartamentoModel> GetDepartamentos()
        {
            return AppDb.Departamento.ToList();
        }

        public DepartamentoModel GetDepartamento(int id)
        {
            return AppDb.Departamento.FirstOrDefault(x => x.DepartamentoId == id);
        }

        public void CreateDepartamento(DepartamentoModel model)
        {
            AppDb.Departamento.Add(model);
            AppDb.SaveChanges();
        }

        public bool DeleteDepartamento(int id)
        {
            var departamento = AppDb.Departamento.FirstOrDefault(x => x.DepartamentoId == id);
            if (departamento != null)
            {
                var requisicao = AppDb.Requisicoes.FirstOrDefault(x => x.DepartamentoId == id.ToString());
                if (requisicao == null)
                {
                    AppDb.Departamento.Remove(departamento);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void EditDepartamento(DepartamentoModel model)
        {
            AppDb.Departamento.Update(model);
            AppDb.SaveChanges();
        }
    }
}
