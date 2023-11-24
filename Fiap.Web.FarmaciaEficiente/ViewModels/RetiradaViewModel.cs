using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Models.Enum;

namespace Fiap.Web.FarmaciaEficiente.ViewModels
{
    public class RetiradaViewMode
    {
        public int MedicamentoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public StatusEnum Status { get; set; }
        public Boolean Disponivel { get; set; }
        public int Quantidade { get; set; }
        public int EstoqueId { get; set; }
        public Posto Posto { get; set; }    
    }
}
