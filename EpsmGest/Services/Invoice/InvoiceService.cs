using EpsmGest.Data;
using EPSMGest.Models;

namespace EPSMGest.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        public readonly EpsmGestDbContext AppDb;

        public InvoiceService(EpsmGestDbContext Db)
        {
            AppDb = Db;
        }

        public List<InvoiceModel> GetInvoices()
        {
            return AppDb.Invoice.ToList();
        }

        public InvoiceModel? GetInvoice(int id)
        {
            return AppDb.Invoice.FirstOrDefault(x => x.InvoiceId == id);
        }

        public void CreateInvoice(InvoiceModel model)
        {
            AppDb.Invoice.Add(model);
            AppDb.SaveChanges();
        }

        public void DeleteInvoice(int id)
        {
            var model = AppDb.Invoice.FirstOrDefault(x => x.InvoiceId == id);
            if (model != null)
            {
                AppDb.Invoice.Remove(model);
                AppDb.SaveChanges();
            }
        }

        public void EditInvoice(InvoiceModel model)
        {
            AppDb.Invoice.Update(model);
            AppDb.SaveChanges();
        }
    }
}
