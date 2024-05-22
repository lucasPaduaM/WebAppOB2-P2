using Dominio;
using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;

namespace Social.NetWork.Controllers
{
    public class PublicarComentarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PublicarComentario(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult PublicarComentario(int idPost, Comentario comentario)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            if (Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado")).estadoAdmin == EstadoAdmin.NO_BANEADO)
            {
                try
                {
                    Miembro autor = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
                    comentario.SetAutor(autor);
                    comentario.SetFecha(DateTime.Now);
                    Sistema.Instancia.AddComentario(idPost, comentario, autor);
                    RedirectToAction("Index", "VerPost");
                }
                catch (Exception)
                {
                    RedirectToAction("Index", "VerPublicaciones");
                }
                return RedirectToAction("VerPublicaciones", "Index");
            }
            return RedirectToAction("Inicio", "Index", new { mensaje = "Te han baneado!" });
        }
    }
}