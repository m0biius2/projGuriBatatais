using Microsoft.AspNetCore.Mvc;

namespace projGuriBatatais.Controllers
{
    public class InicialController : Controller
    {
        public IActionResult Index()
        {
            return View("ViewIndex");
        }

        public IActionResult IndexPublico()
        {
            return View("ViewIndexPublico");
        }

        public IActionResult IndexAluno()
        {
            return View("ViewIndexAluno");
        }

        public IActionResult IndexAdm()
        {
            return View("ViewIndexAdm");
        }
    }
}
