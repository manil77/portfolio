using Core.Entities.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController: ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly SignInManagerService _signInManager;

        public PortfolioController(ILogger<PortfolioController> logger,
                SignInManagerService signInManager,
                JWTAuthService jwtAuthManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Route("api/test")]
        public string Test() 
        {
            return "Test";
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public async Task<ActionResult> LoginAPI(LoginRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _signInManager.SignIn(request.UserName, request.Password);

            if (!result.Success) return Unauthorized();

            _logger.LogInformation($"User [{request.UserName}] logged in the system.");

            return Ok(new LoginResult
            {
                UserName = result.User.Email,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }
    }
}
