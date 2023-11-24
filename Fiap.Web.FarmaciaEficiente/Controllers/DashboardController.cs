using Fiap.Web.FarmaciaEficiente.Models;
using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


[Authorize]
[TypeFilter(typeof(FiltroAuthorization))]
[Route("dashboard")]
public class DashboardController : Controller
{
    private readonly FarmaciaContext _context;

    public DashboardController(FarmaciaContext context)
    {
        _context = context;
    }

    private Usuario GetUsuarioFromHttpContext()
    {
        return HttpContext.Items["Usuario"] as Usuario;
    }

    public IActionResult Index()
    {
        ViewData["Usuario"] = GetUsuarioFromHttpContext();
        return RedirectToAction("index", "posto");
    }
}
