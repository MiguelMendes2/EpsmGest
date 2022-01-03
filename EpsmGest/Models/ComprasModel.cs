using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class ComprasModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IdCompra { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Item { get; set; }

        public bool Aprovacao1 { get; set; }

        public bool Aprovacao2 { get; set; }

        public string? IdRequisicao { get; set; }

        public string? Pagamento { get; set; }

        public string? Observacoes { get; set; }

    }
}
