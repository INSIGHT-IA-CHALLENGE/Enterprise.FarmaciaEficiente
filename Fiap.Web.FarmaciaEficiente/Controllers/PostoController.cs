using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Models.Enum;
using Fiap.Web.FarmaciaEficiente.ViewModels;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{

    [Authorize]
    [TypeFilter(typeof(FiltroAuthorization))]
    [Route("dashboard/posto")]

    public class PostoController : Controller
    {

        private FarmaciaContext _context;

        public PostoController(FarmaciaContext context)
        {
            _context = context;
        }

        private Usuario GetUsuarioFromHttpContext()
        {
            return HttpContext.Items["Usuario"] as Usuario;
        }

        public IActionResult Index(string filter = "posto", string query = "")
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var postos = new List<Posto> { };

            Console.WriteLine("query", query);

            if (filter == "medicamento")
            {
                postos = _context.Estoques
                    .Where(e => e.Medicamento.Nome.Contains(query) && e.Status == StatusEnum.ATIVO)
                    .Select(e => e.Posto)
                    .Distinct()
                    .Include(posto => posto.Endereco)
                    .ToList();
            }
            else if (filter == "posto")
            {
                postos = _context.Postos
                    .Where(p => p.Nome.Contains(query) || string.IsNullOrEmpty(query))
                    .Include(p => p.Endereco)
                    .ToList();

            }


            var postosViewModel = new List<PostoViewModel>();

            foreach (var posto in postos)
            {
                var postoViewModel = new PostoViewModel
                {
                    PostoId = posto.PostoId,
                    Nome = posto.Nome,
                    Endereco = posto.Endereco,
                    Descricao = posto.Descricao,
                    QuantidadeMedicamentos = _context.Estoques
                        .Count(e => e.Posto.PostoId == posto.PostoId && e.Status == StatusEnum.ATIVO)
                };

                postosViewModel.Add(postoViewModel);
            }

            return View(postosViewModel);
        }

    }
}
