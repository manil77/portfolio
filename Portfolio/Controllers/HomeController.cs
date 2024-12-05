using Application.ApplicationInterfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Middleware.Models;
using System.Diagnostics;

namespace Middleware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUnitOfWork _appUnitOfWork;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            throw new InvalidOperationException("This is a test exception to trigger HandleExceptionAsync.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
