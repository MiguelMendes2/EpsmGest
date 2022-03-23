using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class DepartmentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartamentId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<RequisitionModel>? Requisitions { get; set;}
    }
}
