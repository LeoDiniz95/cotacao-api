using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using cotacao_api.General;

namespace cotacao_api.Models
{
    public class CotacaoItemDM
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idcotacao")]
        public int IdCotacao { get; set; }

        [Required(ErrorMessage = Messages.Errors.DescricaoRequired)]
        [MinLength(1, ErrorMessage = Messages.Errors.Descricaolength)]
        [Column("descricao")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = Messages.Errors.numeroItemRequired)]
        [Column("numeroitem")]
        public int NumeroItem { get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = Messages.Errors.QuantidadeRequired)]
        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("marca")]
        public string Marca { get; set; }

        [Column("unidade")]
        public string Unidade { get; set; }

        [Required(ErrorMessage = Messages.Errors.required)]
        [StringLength(1)]
        [Column("status")]
        public string Status { get; set; }
    }
}
