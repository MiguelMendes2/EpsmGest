using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class RequisitionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequisicaoId { get; set; }

        [Required]
        [ForeignKey("Departament")]
        public int DepartamentId { get; set; }
        public DepartmentModel Departament { get; set; }

        [Required]
        public string Applicant { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string? FilesName { get; set; }

		public bool Delivered { get; set; }

		public ICollection<PurchaseModel> Purchase { get; set; }
    }
}
