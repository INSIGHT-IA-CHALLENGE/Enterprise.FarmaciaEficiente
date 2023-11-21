using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fiap.Web.FarmaciaEficiente.Models.Enum;

namespace Fiap.Web.FarmaciaEficiente.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnderecoId { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = "Logradouro deve ter entre 1 e 150 caracteres")]
        [Required(ErrorMessage = "Logradouro é obrigatório")]
        public string? Logradouro { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = "Bairro deve ter entre 1 e 150 caracteres")]
        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string? Bairro { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = "Cidade deve ter entre 1 e 150 caracteres")]
        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string? Cidade { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter 2 caracteres")]
        [Required(ErrorMessage = "UF é obrigatória")]
        public string? Uf { get; set; }

        [RegularExpression(@"\d{5}-\d{3}", ErrorMessage = "CEP inválido")]
        [Required(ErrorMessage = "CEP é obrigatório")]
        public string? Cep { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "Número deve ter entre 1 e 10 caracteres")]
        [Required(ErrorMessage = "Número é obrigatório")]
        public string? Numero { get; set; }

        
        public string? Complemento { get; set; } = "Sem complemento";

        [Required(ErrorMessage = "Status é obrigatório")]
        public StatusEnum Status { get; set; } = StatusEnum.ATIVO;
    }
}
