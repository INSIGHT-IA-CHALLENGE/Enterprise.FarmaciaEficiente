using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Fiap.Web.FarmaciaEficiente.ViewModels;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    [Authorize]
    [TypeFilter(typeof(FiltroAuthorization))]
    [Route("dashboard/estoque")]
    public class EstoqueController : Controller
    {
        private FarmaciaContext _context;

        public EstoqueController(FarmaciaContext context)
        {
            _context = context;
        }

        private Usuario GetUsuarioFromHttpContext()
        {
            return HttpContext.Items["Usuario"] as Usuario;
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create(int postoId)
        {
            ViewData["Usuario"] = GetUsuarioFromHttpContext();
            var posto = _context.Postos.Include(p => p.Endereco).FirstOrDefault(p => p.PostoId == postoId);

            if (posto == null)
            {
                return NotFound();
            }


            var medicamentos = _context.Medicamentos.Where(e => e.Status == Models.Enum.StatusEnum.ATIVO).ToList();
            var viewModel = new AddEstoqueViewModel
            {
                Posto = posto,
                MedicamentosDisponiveis = medicamentos
            };

         

            return View(viewModel);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(AddEstoqueViewModel model)
        {
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var medicamento = _context.Medicamentos.FirstOrDefault(p => p.MedicamentoId == model.MedicamentoId);
            var posto = _context.Postos.Include(p => p.Endereco).FirstOrDefault(p => p.PostoId == model.Posto.PostoId);

            var estoque = new Estoque
            {
                Medicamento = medicamento,
                PostoId = model.Posto.PostoId,
                Quantidade = model.Quantidade,
                Status = Models.Enum.StatusEnum.ATIVO
            };

      
            _context.Estoques.Add(estoque);
            _context.SaveChanges();

            TempData["msg"] = "Estoque adicionado com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index", "Posto");
        }

  
    }
}
