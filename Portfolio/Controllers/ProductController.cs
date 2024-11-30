using Application.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAppUnitOfWork _appUnitOfWork;

        public ProductController(IAppUnitOfWork appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        public IActionResult Index()
        {
            
            var result = _appUnitOfWork.Product.GetAllProducts();
            return View();
        }
    }
}
