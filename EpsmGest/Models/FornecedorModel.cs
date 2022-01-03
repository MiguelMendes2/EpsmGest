using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class FornecedorModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FornecedorId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string? Morada { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]\d{8}$", ErrorMessage = "NIF inválido")]
        public int NIF { get; set; }
    }
}
