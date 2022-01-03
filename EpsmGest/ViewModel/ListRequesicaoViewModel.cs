using System.ComponentModel.DataAnnotations;

namespace EpsmGest.ViewModel
{
    public class ListRequesicaoViewModel
    {
        [Key]
        public string? RequisicaoId { get; set; }

        public string? DepartamentoId { get; set; }

        public string? Requerente { get; set; }

        public string? Descricao { get; set; }

        public DateTime date { get; set; }
    }
}
