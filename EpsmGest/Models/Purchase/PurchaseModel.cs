using EpsmGest.Models;
using EpsmGest.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class PurchaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string PurchaseId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal TotalPrice { get; set; }

		public int Stage { get; set; }

		[ForeignKey("Requisition")]
        public string RequisitionId { get; set; }
        public RequisitionModel Requisition { get; set; }

        public ICollection<PurchaseItemModel> PurchaseItem { get; set; }

        public ICollection<SupplierEvaluationModel>? Evaluations { get; set; }
    }
    /*
    0- Aguardando Consulta de Mercado 
    1- Aguardando Parecer 1
    2- Aguardando Parecer 2
    3- Aguardando Avaliação
    4- Finalizado
    */
}