using AspReactTestApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.Services.AuthService;
using AspReactTestApp.Services.UserService;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login(LoginUserDto request)
        {
            var (response, tokens) = await _authService.LoginUser(request);

            if (response.IsSuccessfull)
            {
                var cookieOptions = new CookieOptions
                {
                    Secure = true, // cookie is to be transmitted over https only
                    HttpOnly = true,
                    Expires = tokens.TokenExpires,
                    SameSite = SameSiteMode.Strict
                };

                // Set access token in cookie
                Response.Cookies.Append("access_token", tokens.AccessToken, cookieOptions);

                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPost("checkuserexists")]
        public async Task<ActionResult<ResponseDto>> CheckUserExists(LoginUserDto request)
        {
            try
            {
                var result = await _userService.CheckUserExists(request.UserName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseDto>> RefreshToken()
        {
            var (response, tokens) = await _authService.RefreshToken();

            if (response.IsSuccessfull)
            {
                var cookieOptions = new CookieOptions
                {
                    Secure = true, // cookie is to be transmitted over https only
                    HttpOnly = true,
                    Expires = tokens.TokenExpires,
                    SameSite = SameSiteMode.Strict
                };

                // Set access token in cookie
                Response.Cookies.Append("access_token", tokens.AccessToken, cookieOptions);
                return Ok(response);
            }

            return NotFound(response);
        }
    }
}
