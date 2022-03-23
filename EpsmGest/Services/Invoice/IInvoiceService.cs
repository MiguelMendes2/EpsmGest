using EPSMGest.Models;

namespace EPSMGest.Services.Invoice
{
    public interface IInvoiceService
    {
        public List<InvoiceModel> GetInvoices();

        public InvoiceModel GetInvoice(int id);

        public void CreateInvoice(InvoiceModel model);

        public void DeleteInvoice(int id);

        public void EditInvoice(InvoiceModel model);
    }
}
