using Microsoft.AspNetCore.Mvc;

namespace SuaAplicacao.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}
