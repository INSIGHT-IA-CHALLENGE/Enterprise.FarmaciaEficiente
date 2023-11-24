using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Models.Enum;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Fiap.Web.FarmaciaEficiente.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    [Authorize]
    [TypeFilter(typeof(FiltroAuthorization))]
    [Route("dashboard/retirada")]

    public class RetiradaController : Controller
    {


        private readonly FarmaciaContext _context;

        public RetiradaController(FarmaciaContext context)
        {
            _context = context;
        }

        private Usuario GetUsuarioFromHttpContext()
        {
            return HttpContext.Items["Usuario"] as Usuario;
        }




        public IActionResult Index()
        {
            var usuario = GetUsuarioFromHttpContext();
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var retiradas = _context.Retiradas
                .Include(r => r.Estoque)
                    .ThenInclude(e => e.Posto)
                .Include(r => r.Estoque)
                    .ThenInclude(e => e.Medicamento)
                .Where(e => e.UsuarioId == usuario.UsuarioId)
                .OrderByDescending(e => e.DataCadastro)

                .ToList();

            return View(retiradas);
        }


        [HttpGet]
        [Route("create")]
        public IActionResult Create(int postoId, int medicamentoId)
        {
            ViewData["Usuario"] = GetUsuarioFromHttpContext();
            var posto = _context.Postos.Include(p => p.Endereco).FirstOrDefault(p => p.PostoId == postoId);

            if (posto == null)
            {
                return NotFound();
            }

            var estoqueMedicamento = _context.Estoques
                .Where(e => e.Posto.PostoId == postoId && e.Medicamento.MedicamentoId == medicamentoId)
                .GroupBy(e => e.Medicamento.MedicamentoId)
                .Select(group => new RetiradaViewMode
                {
                    EstoqueId = group.First().EstoqueId,
                    MedicamentoId = group.Key,
                    Nome = group.First().Medicamento.Nome,
                    Descricao = group.First().Medicamento.Descricao,
                    Status = group.First().Medicamento.Status,
                    Quantidade = group.Sum(e => e.Quantidade),
                    Posto = posto
                })
                .FirstOrDefault();


            return View(estoqueMedicamento);
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(RetiradaViewMode model)
        {

            var usuario = GetUsuarioFromHttpContext();

            var retirada = new Retirada
            {
                EstoqueId = model.EstoqueId,
                UsuarioId = usuario.UsuarioId,
            };

            _context.Retiradas.Update(retirada);
            _context.SaveChanges();

            TempData["msg"] = "Retirada adicionado com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index", "Retirada");

        }


        [HttpPost]
        [Route("done")]
        public IActionResult Done(int id)
        {
            var retirada = _context.Retiradas.Find(id);
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            retirada.Status = RetiradasEnum.CONCLUIDO;
            _context.Retiradas.Update(retirada);
            _context.SaveChanges();

            TempData["msg"] = "Retirada concluida com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index", "Retirada");
        }



    }
}
