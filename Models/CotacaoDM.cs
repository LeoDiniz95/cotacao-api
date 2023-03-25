using cotacao_api.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotacao_api.Models
{
    [Table("cotacao")]
    public class CotacaoDM
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.Errors.cnpjCrequired)]
        [MinLength(14, ErrorMessage = Messages.Errors.cnpjlength)]
        [Column("cnpjcomprador")]
        public string CNPJComprador { get; set; }

        [Required(ErrorMessage = Messages.Errors.cnpjFlength)]
        [MinLength(14, ErrorMessage = Messages.Errors.cnpjFRequired)]
        [Column("cnpjfornecedor")]
        public string CNPJFornecedor { get; set; }

        [Required(ErrorMessage = Messages.Errors.numeroCotacaoRequired)]
        [Column("numerocotacao")]
        public int NumeroCotacao { get; set; }

        [Required(ErrorMessage = Messages.Errors.DataCotacaoRequired)]
        [Column("datacotacao")]
        public DateOnly DataCotacao { get; set; }

        [Required(ErrorMessage = Messages.Errors.DataCotacaoRequired)]
        [Column("dataentregacotacao")]
        public DateOnly DataEntregaCotacao { get; set; }

        [Required(ErrorMessage = Messages.Errors.DataCotacaoRequired)]
        [MinLength(8, ErrorMessage = Messages.Errors.cnpjFRequired)]
        [Column("cep")]
        public string CEP { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("complemento")]
        public string Complemento { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }

        [Column("uf")]
        public string UF { get; set; }

        [Column("observacao")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = Messages.Errors.required)]
        [StringLength(1)]
        [Column("status")]
        public string Status { get; set; }
    }
}
