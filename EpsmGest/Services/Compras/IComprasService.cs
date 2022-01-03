using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EPSMGest.Services.Compras
{
    public interface IComprasService
    {
        public List<ComprasModel> GetCompras();

        public ComprasModel GetCompra(int id);

        public List<DropdownViewModel> GetComprasIds();

        public void CreateCompra(ComprasModel model);

        public void EditCompra(ComprasModel model);

        public bool DeleteCompra(int Id);
    }
}
