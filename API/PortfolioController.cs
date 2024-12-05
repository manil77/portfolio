using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController: ControllerBase
    {
        [HttpGet]
        [Route("api/test")]
        public string Test() 
        {
            return "Test";
        }

    }
}
