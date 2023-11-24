using Fiap.Web.FarmaciaEficiente.Models.Enum;

namespace Fiap.Web.FarmaciaEficiente.ViewModels
{
    public class MedicamentoViewModel
    {
        public int MedicamentoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }
        public Boolean Disponivel { get; set; }
        public int Quantidade { get; set; }
    }
}
