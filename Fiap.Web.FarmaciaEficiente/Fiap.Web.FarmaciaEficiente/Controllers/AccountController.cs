using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.FarmaciaEficiente.Controllers
{
    public class AccountController : Controller
    {
 
        private readonly FarmaciaContext _context;

        public AccountController(FarmaciaContext context)
        {
      
            _context = context;
        }



        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario model)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == model.Email);

            if (user != null && user.PasswordHash == model.PasswordHash)
            {

                TempData["user"] = user.UsuarioId;
                return RedirectToAction("index", "dashboard");
            }

            TempData["error"] = "Email ou senha incorretos!";

            return View(model);

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(Usuario model)
        {

            _context.Usuarios.Add(model);
            _context.SaveChanges();
    
            TempData["msg"] = "Usuario cadastrado!";
            return RedirectToAction("login");
        }

     

    }
}
