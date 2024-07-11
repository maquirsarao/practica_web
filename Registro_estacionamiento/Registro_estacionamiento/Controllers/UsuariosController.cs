using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Registro_estacionamiento.Data;
using System.Security.Claims;
using Registro_estacionamiento.BR;

namespace Registro_estacionamiento.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosBR _UsuariosBR;

        public UsuariosController(UsuariosBR usuariosBR)
        {
            _UsuariosBR=usuariosBR;
        }

        [HttpGet]
        [Route("Usuarios/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nombreUsuario, string contrasena)
        {
            if (ModelState.IsValid)
            {
                var usuario = _UsuariosBR.ObtenerUsuarioPorNombre(nombreUsuario);

                if (usuario != null && usuario.Contrasena == contrasena && usuario.Activo)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, nombreUsuario)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos, o cuenta inactiva.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
