using EpsmGest.ViewModel;
using EpsmGest.ViewModel.Purchase;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Models;

namespace EPSMGest.Services.Purchase
{
    public interface IPurchaseService
    {
        public List<PurchaseModel> GetPurchases();

        public PurchaseModel? GetPurchase(int id);

        public List<DropdownViewModel> GetPurchasesIds();

        public Task CreatePurchase(CreateRequisitionViewModel model);

        public void EditPurchase(PurchaseModel model);

        public bool DeletePurchase(int Id);

        public NecessidadeViewModel? GetNecessidade(int Id);

        public bool EditNecessidade(NecessidadeViewModel model);

        public ConsultaMercadoViewModel? GetConsultaMercado(int id);

        public bool EditConsultaMercado(ConsultaMercadoViewModel model);

        public bool CloseConsultaMercado(int id);

        public bool SetParecer1(ConsultaMercadoViewModel model);

        public bool CloseParecer1(int id);

        public bool SetParecer2(ConsultaMercadoViewModel model);

        public bool CloseParecer2(int id);

        public FornecedorEvaluationViewModel? GetFornecedorEvaluation(int id);

        public bool SetFornecedorEvaluation(FornecedorEvaluationViewModel model);
    }
}
