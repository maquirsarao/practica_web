using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Registro_estacionamiento.Utils
{
    public class VerificarAutenticacionAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Verifica si el usuario está autenticado
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Si no está autenticado, redirecciona a la página de login
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Usuarios",
                        action = "Login"
                    }));
            }
        }
    }
}
