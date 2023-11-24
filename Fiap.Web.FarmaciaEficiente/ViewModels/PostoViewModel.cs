using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Models.Enum;

namespace Fiap.Web.FarmaciaEficiente.ViewModels
{
    public class PostoViewModel
    {
        public int PostoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public Endereco? Endereco { get; set; }
        public Estoque Estoque { get; set; }
        public List<MedicamentoViewModel>? Medicamentos { get; set; }
        public int QuantidadeMedicamentos { get; set; }
        public int ReturnUrl { get; set; }
        public StatusEnum Status { get; set; }
    }


}
