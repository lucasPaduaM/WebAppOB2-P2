using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppOB2P2.Controllers
{
    public class InvitacionesController : Controller
    {
        public IActionResult CrearInvitacion(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }
        public IActionResult VerAmigos()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            Miembro usuarioSolicitante = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
            List<Miembro> listaAmigos = usuarioSolicitante.GetListaAmigos();
            return View(listaAmigos);
        }

        public IActionResult VerInvitacion()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            Miembro usuarioSolicitante = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
            List<Invitacion> invitacionesLogueado = Sistema.Instancia.GetInvitacionesPorMiembro(usuarioSolicitante);
            return View(invitacionesLogueado);
        }

        public IActionResult AceptarInvitacion(string emailSolicitante)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            Miembro miembroLoginTemp = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
            string emailSolicitado = miembroLoginTemp.Email;
            Invitacion invitacion = Sistema.Instancia.EncontrarInvitacion(emailSolicitado, emailSolicitante);
            invitacion.estado = Estados.APROBADA;
            Sistema.Instancia.AddAmigo(emailSolicitado, emailSolicitante);

            return RedirectToAction("VerInvitacion", "Invitaciones");
        }

        public IActionResult RechazarInvitacion(string emailSolicitante)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            Miembro miembroLoginTemp = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
            string emailSolicitado = miembroLoginTemp.Email;
            Invitacion invitacion = Sistema.Instancia.EncontrarInvitacion(emailSolicitado, emailSolicitante);
            invitacion.estado = Estados.RECHAZADA;
            return RedirectToAction("VerInvitacion", "Invitaciones");
        }

        public IActionResult EnviarInvitacion(string usuarioSolicitado)
        {
            ViewBag.usuarioSolicitado = usuarioSolicitado;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            return View();
        }

        [HttpPost]
        public IActionResult CrearInvitacion(Invitacion invitacion, string usuarioSolicitado)
        {
            ViewBag.usuarioSolicitado = usuarioSolicitado;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            if (Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado")).estadoAdmin == Dominio.EstadoAdmin.NO_BANEADO)
            {
                try
                {
                    Miembro usuarioSolicitante = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
                    Estados estadoInvitacion = Estados.PENDIENTE_APROBACION;

                    invitacion.estado = estadoInvitacion;
                    invitacion.Solicitado = Sistema.Instancia.GetUsuario(usuarioSolicitado);
                    invitacion.Solicitante = Sistema.Instancia.GetUsuario(usuarioSolicitante.GetEmail());
                    invitacion.Solicitado.GetListaPendientes().Add(invitacion);
                    Sistema.Instancia.AddInvitacion(invitacion);

                    ViewBag.usuarioSolicitado = usuarioSolicitado;
                    return RedirectToAction("EnviarInvitacion", "Invitaciones");

                }
                catch (Exception)
                {
                    return View();
                }
            }
            return RedirectToAction("Inicio", "Index", new { mensaje = "Te han baneado!" });
        }
    }
}