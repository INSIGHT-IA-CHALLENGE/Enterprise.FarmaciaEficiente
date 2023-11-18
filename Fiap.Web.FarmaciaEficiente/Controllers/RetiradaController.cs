using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    [Authorize]
    [TypeFilter(typeof(FiltroAuthorization))]
    [Route("dashboard/retirada")]

    public class RetiradaController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
