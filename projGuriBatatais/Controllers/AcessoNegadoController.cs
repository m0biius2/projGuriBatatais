using Microsoft.AspNetCore.Mvc;

namespace projGuriBatatais.Controllers
{
    public class AcessoNegadoController : Controller
    {
        public IActionResult AcessoNegado()
        {
            return View("ViewAcessoNegado");
        }
    }
}
