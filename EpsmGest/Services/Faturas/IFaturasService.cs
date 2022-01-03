using EPSMGest.Models;

namespace EPSMGest.Services.Faturas
{
    public interface IFaturasService
    {
        public List<FaturasModel> GetFaturas();

        public FaturasModel GetFatura(int id);

        public void CreateFatura(FaturasModel model);

        public void DeleteFatura(int id);

        public void EditFatura(FaturasModel model);
    }
}
