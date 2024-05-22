using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;

namespace WebAppOB2P2.Controllers
{

    public class LoginController : Controller
    {
        public IActionResult Login(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            List<Usuario> usuarios = Sistema.Instancia.GetUsuarios();

            int pos = Sistema.Instancia.GetPosicionEmal(email);
            if (Sistema.Instancia.DoLogin(email, password))
            {
                HttpContext.Session.SetString("usuarioLogueado", usuarios[pos].GetEmail());
                Usuario usuarioLogueado = Sistema.Instancia.GetUsuarioUsuario(HttpContext.Session.GetString("usuarioLogueado"));

                if (!usuarioLogueado.GetType().ToString().Contains("Administrador"))
                {
                    if (Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado")).estadoAdmin == EstadoAdmin.BANEADO)
                    {
                        return RedirectToAction("Inicio", "Index", new { mensaje = "Tu usuario ha sido baneado, varias de las funcionalidades de la aplicacion se veran restringidas, mejora tu comportamiento la proxima vez." });
                    }
                    else
                    {
                        return RedirectToAction("Inicio", "Index");
                    }
                }
                else
                {
                    return RedirectToAction("VerMiembrosAdmin", "Index");
                }
            }
            return RedirectToAction("Login", new { mensaje = "Email o contrasenia incorrectos!" });
        }
        public IActionResult Logout(string mensaje)
        {
            HttpContext.Session.SetString("usuarioLogueado", "");
            return RedirectToAction("Login");
        }
    }
}