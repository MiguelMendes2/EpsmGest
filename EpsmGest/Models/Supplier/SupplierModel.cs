using EpsmGest.Models;
using EpsmGest.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class SupplierModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Address { get; set; }

        [RegularExpression(@"^[0-9]\d{8}$", ErrorMessage = "NIF inválido")]
        public int NIF { get; set; }

        public ICollection<SupplierEvaluationModel>? SupplierEvaluations { get; set; }

        public ICollection<PurchaseItemModel>? ComprasItems { get; set; }
    }
}
