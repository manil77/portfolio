using Core.Entities.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Portfolio.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManagerService _signInManager;

        public AuthController(ILogger<AuthController> logger,
                                SignInManagerService signInManager,
                                JWTAuthService jwtAuthManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        //[Route("login")]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginRequest request)
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

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _signInManager.RefreshToken(request.AccessToken, request.RefreshToken);

            if (!result.Success) return Unauthorized();

            return Ok(new LoginResult
            {
                UserName = result.User.Email,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }



    }
}
