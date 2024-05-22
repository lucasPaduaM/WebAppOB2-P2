using Dominio;
using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;

namespace Social.NetWork.Controllers
{
    public class AccionesAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BanearPost(int idPost)
        {
            Sistema.Instancia.GetPostPorId(idPost).estadoAdmin = EstadoAdmin.BANEADO;
            return RedirectToAction("VerPublicacionesAdmin", "Index");
        }

        public IActionResult BanearUsuario(string emailUsuario)
        {
            Sistema.Instancia.GetUsuario(emailUsuario).estadoAdmin = EstadoAdmin.BANEADO;
            return RedirectToAction("VerMiembrosAdmin", "Index");
        }

        public IActionResult BanearComentario(int idComentario)
        {
            Sistema.Instancia.GetComentarioPorId(idComentario).estadoAdmin = EstadoAdmin.BANEADO;
            return RedirectToAction("VerPublicacionesAdmin", "Index");
        }
    }
}
