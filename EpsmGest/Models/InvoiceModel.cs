using EPSMGest.Models.Supplier;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class InvoiceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        public int InvoiceNumber { get; set; }

        [RegularExpression(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*\.[0-9]{2}$", ErrorMessage = "Deve utilizar o formato 100,00")]
        public decimal? TotalPrice { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Supplier")]
        public int? SupplierId { get; set; }
        public SupplierModel? Supplier { get; set; }

        public int? IdPurchase { get; set; }

        public string? Observacoes { get; set; }
    }
}
