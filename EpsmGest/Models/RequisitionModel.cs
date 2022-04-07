using EPSMGest.Models.Intervention;
using EPSMGest.Models.Purchase;
using EPSMGest.Models.Space;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class RequisitionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequisicaoId { get; set; }

        public int Type { get; set; }

        [Required]
        [ForeignKey("Departament")]
        public int DepartamentId { get; set; }
        public DepartmentModel Departament { get; set; }

        [Required]
        public string Applicant { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string? FilesName { get; set; }

		public int Delivered { get; set; }

		public ICollection<PurchaseModel>? Purchase { get; set; }

		public ICollection<RequestInterventionModel>? RequestInterventions { get; set; }

		public ICollection<RequestSpaceModel>? RequestSpaces { get; set; }
    }

    /* TYPES Codes:
        0 -> Req. Purchase
        1 -> Req. Intervention
        2 -> Req. Space
        3 -> Req. Vehicle

       Delivered Code:
        0 -> Awaiting
        1 -> Rejected
        2 -> Concluded
        
     */

}
