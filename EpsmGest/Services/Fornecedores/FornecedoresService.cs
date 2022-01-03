using EpsmGest.Data;
using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EPSMGest.Services.Fornecedores
{
    public class FornecedoresService : IFornecedoresService
    {
        public readonly EpsmGestDbContext AppDb;

        public FornecedoresService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<FornecedorModel> GetFornecedores()
        {
            return AppDb.Fornecedor.ToList();
        }

        public FornecedorModel GetFornecedor(int id)
        {
            return AppDb.Fornecedor.FirstOrDefault(x => x.FornecedorId == id);
        }

        public List<DropdownViewModel> GetFornecedoresIds()
        {
            return AppDb.Fornecedor.Select(x => new DropdownViewModel { Id = x.FornecedorId.ToString(), Name = x.Nome }).ToList();
        }

        public void CreateFornecedor(FornecedorModel model)
        {
            AppDb.Fornecedor.Add(model);
            AppDb.SaveChanges();
        }

        public bool DeleteFornecedor(int id)
        {
            var fornecedor = AppDb.Fornecedor.FirstOrDefault(x => x.FornecedorId == id);
            if (fornecedor != null)
            {
                var faturas = AppDb.Fatura.FirstOrDefault(x => x.Fornecedor == id.ToString());
                if (faturas == null)
                {
                    AppDb.Fornecedor.Remove(fornecedor);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void EditFornecedor(FornecedorModel model)
        {
            AppDb.Fornecedor.Update(model);
            AppDb.SaveChanges();
        }
    }
}
