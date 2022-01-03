using EpsmGest.ViewModel;
using EPSMGest.Models;

namespace EPSMGest.Services.Requisicao
{
    public interface IRequisicaoService
    {
        public List<RequisicoesModel> GetRequesicoes();

        public List<RequisicoesModel> GetUserRequesicoes(string userName);

        public RequisicoesModel GetRequesicao(string Id);

        public List<DropdownViewModel> GetRequesicaoIds();

        public void CreateRequesicao(RequisicoesModel model);

        public void EditRequesicao(RequisicoesModel model);

        public bool DeleteRequesicao(string Id);
    }
}
