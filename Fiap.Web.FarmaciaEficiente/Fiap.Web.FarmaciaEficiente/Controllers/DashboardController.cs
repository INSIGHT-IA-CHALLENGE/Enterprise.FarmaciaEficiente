using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
