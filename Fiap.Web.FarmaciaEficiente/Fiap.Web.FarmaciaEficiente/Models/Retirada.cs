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

        /*Propriedade para representar a chave estrangeira de Usuario*/
        [Required(ErrorMessage = "Usuário é obrigatório")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")] // Define que UsuarioId é a chave estrangeira
        public Usuario Usuario { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
