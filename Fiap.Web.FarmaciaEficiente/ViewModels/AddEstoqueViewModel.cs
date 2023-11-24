using Fiap.Web.FarmaciaEficiente.Models;

namespace Fiap.Web.FarmaciaEficiente.ViewModels
{
    public class AddEstoqueViewModel
    {
        
        public int EstoqueId { get; set; }
        public Posto Posto { get; set; }
        public List<Medicamento> MedicamentosDisponiveis { get; set; }
        public int MedicamentoId { get; set; }
        public int Quantidade { get; set; }
    }
}
