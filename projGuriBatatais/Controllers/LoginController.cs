using Microsoft.AspNetCore.Mvc;

namespace projGuriBatatais.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View("ViewLogin");
        }
    }
}
