using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppOB2P2.Controllers
{
    public class RegistroController : Controller
    {

        public IActionResult Registro(string error)
        {
            ViewBag.Mensaje = error;
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Miembro miembro)
        {
            try
            {
                Sistema.Instancia.AddUsuarioMiembro(miembro);
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Registro", new { error = ex.Message});
            }
        }
    }
}