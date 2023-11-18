using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    [Route("dashboard/retirada")]
    [TypeFilter(typeof(VerificarUsuarioFiltrar))]
    public class RetiradaController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
