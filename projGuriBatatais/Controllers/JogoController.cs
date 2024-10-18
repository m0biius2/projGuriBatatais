using Microsoft.AspNetCore.Mvc;

namespace projGuriBatatais.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Jogar()
        {
            return View("ViewJogar");
        }
    }
}
