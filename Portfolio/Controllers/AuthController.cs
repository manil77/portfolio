using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class AuthController : Controller
    {
        //[Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
