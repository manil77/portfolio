using Core.Entities;
using Core.Entities.Auth;
using Infrastructure.SQLHelper;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public User User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public SignInResult()
        {
            Success = false;
        }

    }
    public class SignInManagerService
    {
        private readonly ILogger<SignInManagerService> _logger;
        //private readonly ApplicationDbContext _ctx;
        private readonly JWTAuthService _JwtAuthService;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly ISQLHelper _sqlHelper;


        public SignInManagerService(ILogger<SignInManagerService> logger,
                             JWTAuthService JWTAuthService,
                             JwtTokenConfig jwtTokenConfig,
                             ISQLHelper sqlHelper)
        {
            _logger = logger;
            _JwtAuthService = JWTAuthService;
            _jwtTokenConfig = jwtTokenConfig;
            _sqlHelper = sqlHelper;
        }

        public async Task<SignInResult> SignIn(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]", userName);

            SignInResult result = new SignInResult();

            if (string.IsNullOrWhiteSpace(userName)) return result;
            if (string.IsNullOrWhiteSpace(password)) return result;

            //DB Call
            //var user = await _ctx.Users.Where(f => f.Email == userName && f.Password == password).FirstOrDefaultAsync();
            //Need to see what kind of data it gives
            var user = _sqlHelper.ExecuteSqlScript<User>($"select * from users where email='{userName}' and password = '{password}';").ToList(); 

            if (user != null)
            {
                var claims = BuildClaims(user[0]);
                result.User = user[0];
                result.AccessToken = _JwtAuthService.BuildToken(claims);
                result.RefreshToken = _JwtAuthService.BuildRefreshToken();

                _sqlHelper.ExecuteRawSqlScript($"insert into refresh_token (user_id, token, issued_at, expires_at) " +
                    $"values ('{user[0].Id}', '{result.RefreshToken}', '{DateTime.Now}', '{DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)}')");
                //_ctx.RefreshTokens.Add(new RefreshToken { UserId = user.Id, Token = result.RefreshToken, IssuedAt = DateTime.Now, ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration) });
                //_ctx.SaveChanges();

                result.Success = true;
            };

            return result;
        }

        public async Task<SignInResult> RefreshToken(string AccessToken, string RefreshToken)
        {

            ClaimsPrincipal claimsPrincipal = _JwtAuthService.GetPrincipalFromToken(AccessToken);
            SignInResult result = new SignInResult();

            if (claimsPrincipal == null) return result;

            string id = claimsPrincipal.Claims.First(c => c.Type == "id").Value;

            //var user = await _ctx.Users.FindAsync(Convert.ToInt32(id));
            var user = (User)_sqlHelper.ExecuteSqlScript<User>($"select * from user where id = {id}");

            if (user == null) return result;

            //var token = await _ctx.RefreshTokens
            //        .Where(f => f.UserId == user.Id
            //                && f.Token == RefreshToken
            //                && f.ExpiresAt >= DateTime.Now)
            //        .FirstOrDefaultAsync();

            var token = _sqlHelper.ExecuteSqlScript<RefreshToken>($"select * from refresh_token where id = {user.Id} and " +
                $"token = {RefreshToken} and expires_at = {DateTime.Now}");

            if (token == null) return result;

            var claims = BuildClaims(user);

            result.User = user;
            result.AccessToken = _JwtAuthService.BuildToken(claims);
            result.RefreshToken = _JwtAuthService.BuildRefreshToken();

            //_ctx.RefreshTokens.Remove(token);
            _sqlHelper.ExecuteRawSqlScript($"delete from refresh_token where token = {token}");
            //_ctx.RefreshTokens.Add(new RefreshToken { UserId = user.Id, Token = result.RefreshToken, IssuedAt = DateTime.Now, ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration) });
            _sqlHelper.ExecuteRawSqlScript($"insert into refresh_token values ('{result.RefreshToken}', {user.Id}, {DateTime.Now}, {DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)} ) ");
            //_ctx.SaveChanges();

            result.Success = true;

            return result;
        }

        private Claim[] BuildClaims(User user)
        {
            //User is Valid
            var claims = new[]
            {
                new Claim("id",user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email)
 
                //Add Custom Claims here
            };

            return claims;
        }


    }
}
