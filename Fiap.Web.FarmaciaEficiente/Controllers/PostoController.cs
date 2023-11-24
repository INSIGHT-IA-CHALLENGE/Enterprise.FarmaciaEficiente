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

        [HttpGet]
        public IActionResult Index(string filter = "posto", string query = "")
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var usuario = GetUsuarioFromHttpContext();

            var postos = new List<Posto> { };


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
                var quantidadeTotalMedicamentos = _context.Estoques
                    .Where(e => e.Posto.PostoId == posto.PostoId && e.Status == StatusEnum.ATIVO)
                    .Sum(e => e.Quantidade);


                var postoViewModel = new PostoViewModel
                {
                    PostoId = posto.PostoId,
                    Nome = posto.Nome,
                    Endereco = posto.Endereco,
                    Descricao = posto.Descricao,
                    Status = posto.Status,
                    QuantidadeMedicamentos = quantidadeTotalMedicamentos
                };
                postosViewModel.Add(postoViewModel);



            }

            return View(postosViewModel);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            return View();

        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(Posto model)
        {
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            _context.Postos.Add(model);
            _context.SaveChanges();

            TempData["msg"] = "Posto cadastrado!";
            TempData["color"] = "success";
            return RedirectToAction("index");
        }



        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int id)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var posto = _context.Postos.Find(id);
            _context.Postos.Remove(posto);
            _context.SaveChanges();
            TempData["msg"] = "Posto removido com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("changeStatus")]
        public IActionResult ChangeStatus(int id)
        {
            var posto = _context.Postos.Find(id);
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            posto.Status = (posto.Status == StatusEnum.ATIVO) ? StatusEnum.INATIVO : StatusEnum.ATIVO;
            _context.Postos.Update(posto);
            _context.SaveChanges();

            TempData["msg"] = "Status do posto alterado com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index");
        }



        [HttpGet]
        [Route("update")]
        public IActionResult Update(int id)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var posto = _context.Postos
                            .Include(p => p.Endereco).First(p => p.PostoId == id);
            return View(posto);
        }



        [HttpGet]
        [Route("detail")]
        public IActionResult Detail(int id)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();
            var posto = _context.Postos
            .Include(p => p.Endereco)
            .FirstOrDefault(p => p.PostoId == id);

            if (posto == null)
            {
                return NotFound();
            }



            var medicamentosRelacionados = _context.Estoques
                .Include(e => e.Medicamento)
                .Where(e => e.Posto.PostoId == posto.PostoId)
                .GroupBy(e => e.MedicamentoId)
                .Select(group => new MedicamentoViewModel
                {
                    MedicamentoId = group.Key,
                    Nome = group.First().Medicamento.Nome,
                    Descricao = group.First().Medicamento.Descricao,
                    Status = group.First().Medicamento.Status,
                    Quantidade = group.Sum(e => e.Quantidade),
                    Disponivel = group.Any(e => e.Medicamento.Status == StatusEnum.ATIVO && e.Quantidade > 0)
                }).ToList();

            var quantidadeMedicamentosAtivos = medicamentosRelacionados
                .Count(m => m.Status == StatusEnum.ATIVO);

            var postoViewModel = new PostoViewModel
            {
                PostoId = posto.PostoId,
                Nome = posto.Nome,
                Endereco = posto.Endereco,
                Descricao = posto.Descricao,
                Status = posto.Status,
                Medicamentos = medicamentosRelacionados,
                ReturnUrl = id,
                QuantidadeMedicamentos = quantidadeMedicamentosAtivos,
                
            };

            return View(postoViewModel);
        }


        [HttpPost]
        [Route("update")]
        public IActionResult Update(Posto posto)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            _context.Postos.Update(posto);
            _context.SaveChanges();
            TempData["msg"] = "Posto atualizado com sucesso!";
            TempData["color"] = "success";
            return RedirectToAction("index");
        }

    }
}
