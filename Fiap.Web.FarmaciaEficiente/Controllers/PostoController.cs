using Fiap.Web.FarmaciaEficiente.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    [Authorize]
    [TypeFilter(typeof(FiltroAuthorization))]
    [Route("dashboard/posto")]
    public class PostoController : Controller
	{

        public IActionResult Index()
		{

            var usuario = HttpContext.Items["Usuario"] as Usuario;
            return View(usuario);
		}
	}
}
