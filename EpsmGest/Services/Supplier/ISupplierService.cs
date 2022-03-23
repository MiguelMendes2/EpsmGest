using EpsmGest.ViewModel;
using EPSMGest.Models;
namespace EPSMGest.Services.Supplier
{
	public interface ISupplierService
	{
        public List<SupplierModel> GetSuppliers();

        public SupplierModel GetSupplier(int id);

        public List<DropdownViewModel> GetSuppliersIds();

        public void CreateSupplier(SupplierModel model);

        public bool DeleteSupplier(int id);

        public void EditSupplier(SupplierModel model);
    }
}