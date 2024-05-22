using Microsoft.AspNetCore.Mvc;
using Social.NetWork.Dominio;

namespace Social.NetWork.Controllers
{
    public class BuscarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
