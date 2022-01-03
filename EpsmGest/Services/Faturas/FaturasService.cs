using EpsmGest.Data;
using EPSMGest.Models;

namespace EPSMGest.Services.Faturas
{
    public class FaturasService : IFaturasService
    {
        public readonly EpsmGestDbContext AppDb;

        public FaturasService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<FaturasModel> GetFaturas()
        {
            return AppDb.Fatura.ToList();
        }

        public FaturasModel GetFatura(int id)
        {
            return AppDb.Fatura.FirstOrDefault(x => x.FaturasId == id);
        }

        public void CreateFatura(FaturasModel model)
        {
            AppDb.Fatura.Add(model);
            AppDb.SaveChanges();
        }

        public void DeleteFatura(int id)
        {
            var model = AppDb.Fatura.FirstOrDefault(x => x.FaturasId == id);
            if (model != null)
            {
                AppDb.Fatura.Remove(model);
                AppDb.SaveChanges();
            }
        }

        public void EditFatura(FaturasModel model)
        {
            AppDb.Fatura.Update(model);
            AppDb.SaveChanges();
        }


    }
}
