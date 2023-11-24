using Fiap.Web.FarmaciaEficiente.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.FarmaciaEficiente.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstoqueId { get; set; }

        [Required(ErrorMessage = "Medicamento é obrigatório")]
        public virtual Medicamento Medicamento { get; set; }

        public int MedicamentoId { get; set; }

        [Required(ErrorMessage = "Posto é obrigatório")]
        public virtual Posto Posto { get; set; }

        public int PostoId { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        public int Quantidade { get; set; } = 0;

        [Required(ErrorMessage = "Status é obrigatório")]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; } = StatusEnum.ATIVO;
    }
}
