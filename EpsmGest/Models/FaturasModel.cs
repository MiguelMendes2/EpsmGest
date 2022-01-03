using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class FaturasModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaturasId { get; set; }

        public int NrFatura { get; set; }

        [RegularExpression(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*\.[0-9]{2}$", ErrorMessage = "Deve utilizar o formato 100.00")]
        public string? ValorTotal { get; set; }

        public DateTime Data { get; set; }

        public string? Fornecedor { get; set; }

        public int? IdCompra { get; set; }

        public string? Observacoes { get; set; }
    }
}
