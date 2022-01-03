using EpsmGest.Data;
using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EPSMGest.Services.Compras
{
    public class ComprasService : IComprasService
    {
        public readonly EpsmGestDbContext AppDb;

        public ComprasService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<ComprasModel> GetCompras()
        {
            return AppDb.Compra.ToList();
        }

        public ComprasModel GetCompra(int id)
        {
            return AppDb.Compra.FirstOrDefault(x => x.Id == id);
        }

        public List<DropdownViewModel> GetComprasIds()
        {
            return AppDb.Compra.Select(x => new DropdownViewModel { Id = x.Id.ToString(), Name = x.IdCompra.ToString() }).ToList();
        }

        public void CreateCompra(ComprasModel model)
        {
            AppDb.Compra.Add(model);
            AppDb.SaveChanges();
        }

        public void EditCompra(ComprasModel model)
        {
            AppDb.Compra.Update(model);
            AppDb.SaveChanges();
        }

        public bool DeleteCompra(int Id)
        {
            var model = AppDb.Compra.FirstOrDefault(x => x.Id == Id);
            if (model != null)
            {
                var dependentrows = AppDb.Fatura.FirstOrDefault(x => x.IdCompra == Id);
                if (dependentrows == null)
                {
                    AppDb.Compra.Remove(model);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
