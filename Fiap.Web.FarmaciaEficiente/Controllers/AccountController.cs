using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(Usuario model)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == model.PasswordHash);

            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Dashboard");
            }
            TempData["msg"] = "Email ou senha incorretos!";
            TempData["color"] = "danger";

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
            TempData["color"] = "success";
            return RedirectToAction("login");
        }



    }
}
