using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Fiap.Web.FarmaciaEficiente.Persistencia;

public class VerificarUsuarioFiltrar : IAsyncActionFilter
{
    private readonly FarmaciaContext _context;

    public VerificarUsuarioFiltrar(FarmaciaContext context)
    {
        _context = context;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Account", action = "Login" })
            );
        }
        else
        {
            var usuarioId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(usuarioId) && int.TryParse(usuarioId, out int userId))
            {
                var usuario = await _context.Usuarios.FindAsync(userId);

                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }

                // Adiciona o objeto 'usuario' ao contexto para estar disponível nas ações
                context.HttpContext.Items["Usuario"] = usuario;
            }
        }

        await next();
    }
}
