using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPSMGest.Models
{
    public class RequisicoesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequisicaoId { get; set; }

        [Required]
        public string DepartamentoId { get; set; }

        [Required]
        public string Requerente { get; set; }

        public string? Descricao { get; set; }

        public DateTime date { get; set; }


    }
}
