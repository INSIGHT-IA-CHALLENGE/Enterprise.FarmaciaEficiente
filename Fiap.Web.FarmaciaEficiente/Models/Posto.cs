using Fiap.Web.FarmaciaEficiente.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.FarmaciaEficiente.Models
{
    [Table("Posto")]
    public class Posto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostoId { get; set; }


        [StringLength(50, MinimumLength = 1, ErrorMessage = "O nome do posto de saúde deve ter entre 1 e 50 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "A descrição do posto de saúde deve ter entre 1 e 50 caracteres")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string? Descricao { get; set; }

        /*"OneToOne"*/
        [Required(ErrorMessage = "Endereco é obrigatório")]
        public Endereco? Endereco { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; } = StatusEnum.ATIVO;
    }
}
