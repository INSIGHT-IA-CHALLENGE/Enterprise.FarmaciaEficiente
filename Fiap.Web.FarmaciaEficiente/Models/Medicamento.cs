using Fiap.Web.FarmaciaEficiente.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Fiap.Web.FarmaciaEficiente.Models.Endereco;

namespace Fiap.Web.FarmaciaEficiente.Models
{
    [Table("Medicamento")]
    public class Medicamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicamentoId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nome deve ter entre 1 e 150 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Descrição deve ter entre 1 e 500 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; } = StatusEnum.ATIVO;
    }
}
