using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [HttpGet]
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PublicAPI()
        {
            return this.Ok("HI this is public API");
        }
    }
}
