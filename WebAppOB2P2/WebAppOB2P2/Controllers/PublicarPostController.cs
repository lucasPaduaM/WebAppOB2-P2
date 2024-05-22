using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace WebAppOB2P2.Controllers
{
    public class PublicarPostController : Controller
    {
        public static Sistema sistema = Sistema.Instancia;

        private IWebHostEnvironment _enviroment;

        public PublicarPostController(IWebHostEnvironment enviroment)
        {
            this._enviroment = enviroment;
        }

        public IActionResult CrearPost(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult CrearPost(Post post, IFormFile imagenPost)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuarioLogueado")))
            {
                return RedirectToAction("Login", "Login", new { mensaje = "Necesita iniciar sesion primero!" });
            }
            if (Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado")).estadoAdmin == Dominio.EstadoAdmin.NO_BANEADO)
            {
                try
                {
                    if (post == null || imagenPost == null)
                    {
                        return RedirectToAction("PublicarPost", "CrearPost", new { mensaje = "Debes completar todos los campos" });
                    }
                    else if (GuardarImagen(imagenPost, post))
                    {
                        Miembro autor = Sistema.Instancia.GetUsuario(HttpContext.Session.GetString("usuarioLogueado"));
                        if (autor == null)
                        {
                            ViewBag.Mensaje = "Usuario no existe";
                            return View();
                        }
                        post.imagen = imagenPost.FileName;
                        post.autor = autor;
                        Sistema.Instancia.AddPost(post);
                    }
                    //Sistema.Instancia.AddPost(fechaPost, autor, titulo, contenido, imagen.FileName, estadoPost);
                    return RedirectToAction("Inicio", "Index");

                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View();
                }
            }
            return RedirectToAction("Inicio", "Index", new { mensaje = "Te han baneado!" });
        }

        private bool GuardarImagen(IFormFile imagen, Post post)
        {
            if (imagen == null || post == null) return false;
            // SUBIR LA IMAGEN
            //ruta física de wwwroot
            string rutaFisicaWwwRoot = _enviroment.WebRootPath;

            string nombreImagen = imagen.FileName;
            //ruta donde se guardan las fotos de las personas
            string rutaFisicaFoto = Path.Combine
            (rutaFisicaWwwRoot, "Imagenes", "Fotos", nombreImagen);
            //FileStream permite manejar archivos
            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar
                using (FileStream f = new FileStream(rutaFisicaFoto, FileMode.Create))
                {
                    //Para archivos grandes o varios archivos usar la versión
                    //asincrónica de CopyTo. Sería: await imagen.CopyToAsync (f);
                    imagen.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO
                post.imagen = nombreImagen;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}