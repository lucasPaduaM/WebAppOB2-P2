using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppOB2P2.Controllers
{
    public class IndexController : Controller
    {
        public static Sistema sistema = Sistema.Instancia;


        public IActionResult Inicio(string mensaje)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            ViewBag.mensaje = mensaje;
            ViewBag.usuario = HttpContext.Session.GetString("usuarioLogueado");
            return View(sistema.GetUsuarios());
        }

        public IActionResult VerPublicaciones()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            return View();
        }
        public IActionResult VerPublicacionesAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            return View();
        }

        public IActionResult VerMiembrosAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            return View();
        }

        public IActionResult VerPublicacionesFilt(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult VerPublicacionesFilt(string valorTitulo, int valorVA)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            try
            {
                List<Publicacion> postsFiltrados = Sistema.Instancia.FiltrarPosts(valorTitulo, valorVA);
                return View(postsFiltrados);
            }
            catch (Exception)
            {
                return RedirectToAction("Inicio", "Index", new { mensaje = "Valores no esperados" });
            }
        }
    }
}
