using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fiap.Web.FarmaciaEficiente.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace Fiap.Web.FarmaciaEficiente.Models
{
    [Table("Usuario")]
    public class Usuario : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Email deve ter entre 1 e 100 caracteres")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Senha deve ter entre 5 e 200 caracteres")]
        public override string PasswordHash { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefone deve ter entre 10 e 15 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Endereco é obrigatório")]
        public Endereco Endereco { get; set; }

        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public StatusEnum Status { get; set; } = StatusEnum.ATIVO;

        [Required(ErrorMessage = "Tipo de Usuário é obrigatório")]
        public TipoUsuarioEnum TipoUsuario { get; set; } = TipoUsuarioEnum.PACIENTE;
    }
}
