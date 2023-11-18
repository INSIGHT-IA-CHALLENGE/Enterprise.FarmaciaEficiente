using Fiap.Web.FarmaciaEficiente.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    [Route("dashboard/posto")]
    [TypeFilter(typeof(VerificarUsuarioFiltrar))]
    public class PostoController : Controller
	{

        public IActionResult Index()
		{

            var usuario = HttpContext.Items["Usuario"] as Usuario;
            return View(usuario);
		}
	}
}
