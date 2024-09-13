using Microsoft.AspNetCore.Mvc;

namespace projGuriBatatais.Controllers
{
    public class InicialController : Controller
    {
        public IActionResult Index()
        {
            return View("ViewIndex");
        }
    }
}
