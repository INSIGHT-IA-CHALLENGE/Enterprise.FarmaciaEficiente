using Fiap.Web.FarmaciaEficiente.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class DashboardController : Controller
{
    private readonly FarmaciaContext _context;

    public DashboardController(FarmaciaContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(usuarioId) && int.TryParse(usuarioId, out int userId))
            {
                // Consulta ao banco de dados para obter o usuário com base no usuarioId
                var usuario = _context.Usuarios
                    .Include(p => p.Endereco)
                    .FirstOrDefault(p => p.UsuarioId == userId);

                if (usuario != null)
                {
                    HttpContext.Items["Usuario"] = usuario;
                    return RedirectToAction("index", "posto");
                }
            }

            // Se o usuário não foi encontrado ou o usuárioId não é válido, redirecione para a página de login
          
        }

        // Se o usuário não estiver autenticado, redirecione para a página de login
        return RedirectToAction("login", "account");
    }
}
