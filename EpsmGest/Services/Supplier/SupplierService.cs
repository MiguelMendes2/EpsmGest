using EpsmGest.Data;
using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EPSMGest.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        public readonly EpsmGestDbContext AppDb;

        public SupplierService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<SupplierModel> GetSuppliers()
        {
            return AppDb.Supplier.ToList();
        }

        public SupplierModel GetSupplier(int id)
        {
            return AppDb.Supplier.FirstOrDefault(x => x.SupplierId == id);
        }

        public List<DropdownViewModel> GetSuppliersIds()
        {
            return AppDb.Supplier.Select(x => new DropdownViewModel { Id = x.SupplierId.ToString(), Name = x.Name }).ToList();
        }

        public void CreateSupplier(SupplierModel model)
        {
            AppDb.Supplier.Add(model);
            AppDb.SaveChanges();
        }

        public bool DeleteSupplier(int id)
        {
            var fornecedor = AppDb.Supplier.FirstOrDefault(x => x.SupplierId == id);
            if (fornecedor != null)
            {
                var faturas = AppDb.Invoice.FirstOrDefault(x => x.SupplierId == id);
                if (faturas == null)
                {
                    AppDb.Supplier.Remove(fornecedor);
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void EditSupplier(SupplierModel model)
        {
            AppDb.Supplier.Update(model);
            AppDb.SaveChanges();
        }
    }
}
