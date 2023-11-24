using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Models.Enum;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{

    [Authorize]
    [TypeFilter(typeof(FiltroAuthorization))]
    [Route("dashboard/medicamento")]
    public class MedicamentoController : Controller
    {



        private FarmaciaContext _context;

        public MedicamentoController(FarmaciaContext context)
        {
            _context = context;
        }

        private Usuario GetUsuarioFromHttpContext()
        {
            return HttpContext.Items["Usuario"] as Usuario;
        }


        public IActionResult Index(string query = "")
        {
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var medicamentos = _context.Medicamentos
                .Where(p => p.Nome.Contains(query) || string.IsNullOrEmpty(query))
                .ToList();

            return View(medicamentos);
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
        public IActionResult Create(Medicamento model)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();
            _context.Medicamentos.Add(model);
            _context.SaveChanges();

            TempData["msg"] = "Medicamento cadastrado!";
            TempData["color"] = "success";


            return RedirectToAction("Index", "Medicamento");
        }


        [HttpPost]
        [Route("changeStatus")]
        public IActionResult ChangeStatus(int id)
        {
            var medicamento = _context.Medicamentos.Find(id);
            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            medicamento.Status = (medicamento.Status == StatusEnum.ATIVO) ? StatusEnum.INATIVO : StatusEnum.ATIVO;
            _context.Medicamentos.Update(medicamento);
            _context.SaveChanges();

            TempData["msg"] = "Status do posto alterado com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int id)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var medicamento = _context.Medicamentos.Find(id);
            _context.Medicamentos.Remove(medicamento);
            _context.SaveChanges();
            TempData["msg"] = "Medicamento removido com sucesso!";
            TempData["color"] = "success";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("update")]
        public IActionResult Update(int id)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            var medicamento = _context.Medicamentos
                            .First(p => p.MedicamentoId == id);
            return View(medicamento);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(Medicamento medicamento)
        {

            ViewData["Usuario"] = GetUsuarioFromHttpContext();

            _context.Medicamentos.Update(medicamento);
            _context.SaveChanges();
            TempData["msg"] = "Posto atualizado com sucesso!";
            TempData["color"] = "success";
            return RedirectToAction("index");
        }

    }
}
