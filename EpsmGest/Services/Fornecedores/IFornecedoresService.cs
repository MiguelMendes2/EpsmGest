using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EPSMGest.Services.Fornecedores
{
    public interface IFornecedoresService
    {
        public List<FornecedorModel> GetFornecedores();

        public FornecedorModel GetFornecedor(int id);

        public List<DropdownViewModel> GetFornecedoresIds();

        public void CreateFornecedor(FornecedorModel model);

        public bool DeleteFornecedor(int id);

        public void EditFornecedor(FornecedorModel model);
    }
}
