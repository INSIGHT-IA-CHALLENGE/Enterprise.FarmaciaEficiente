using Fiap.Web.FarmaciaEficiente.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Web.FarmaciaEficiente.Models
{
    [Table("Retirada")]
    public class Retirada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RetiradaId { get; set; }

        /*ManyToOne*/
        [Required(ErrorMessage = "Estoque é obrigatório")]
        public Estoque Estoque { get; set; }

        public int EstoqueId { get; set; }

        [Required(ErrorMessage = "Usuário é obrigatório")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        [EnumDataType(typeof(StatusEnum))]
        public RetiradasEnum Status { get; set; } = RetiradasEnum.PENDENTE;

        [ForeignKey("UsuarioId")] 
        public Usuario Usuario { get; set; }

        [Column("Dt_Nascimento"), Display(Name = "Data de Nascimento"),
          DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

    }
    }
