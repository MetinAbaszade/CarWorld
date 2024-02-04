using AspReactTestApp.DTO;
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.Services.AuthService;
using AspReactTestApp.Services.UserService;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IUserService userService) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<ActionResult<ResponseDTO>> Login(LoginUserDTO request)
    {
        var (response, token) = await _authService.LogInUser(request);

        if (response.IsSuccessfull)
        {
            var accessTokenCookieOptions = new CookieOptions
            {
                Secure = true, // cookie is to be transmitted over https only
                HttpOnly = true,
                Expires = token.AccessTokenExpires,
                SameSite = SameSiteMode.Strict
            };

            var refreshTokenCookieOptions = new CookieOptions
            {
                Secure = true, // cookie is to be transmitted over https only
                HttpOnly = true,
                Expires = token.RefreshTokenExpires,
                SameSite = SameSiteMode.Strict
            };

            // Set access token in cookie
            Response.Cookies.Append("access_token", token.AccessToken, accessTokenCookieOptions);
            // Set refresh token in cookie
            Response.Cookies.Append("refresh_token", token.RefreshToken, refreshTokenCookieOptions);

            return Ok(response);
        }

        return NotFound(response);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<ResponseDTO>> Logout()
    {
        var responseDTO = await _authService.LogOutUser();

        if (responseDTO.IsSuccessfull)
        {
            return Ok(responseDTO);
        }

        return NotFound(responseDTO);
    }

    [HttpPost("checkuserexists")]
    public async Task<ActionResult<ResponseDTO>> CheckUserExists(LoginUserDTO request)
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

    [HttpGet("isuserauthenticated")]
    public async Task<ActionResult> CheckIsUserAuthenticated()
    {
        try
        {
            var isUserAuthenticated = _authService.IsUserAuthenticated();
            if (isUserAuthenticated)
            {
                return Ok();
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ResponseDTO>> RefreshToken()
    {
        var (response, tokens) = await _authService.RefreshToken();

        if (response.IsSuccessfull)
        {
            var accessTokenCookieOptions = new CookieOptions
            {
                Secure = true, // cookie is to be transmitted over https only
                HttpOnly = true,
                Expires = tokens.AccessTokenExpires,
                SameSite = SameSiteMode.Strict
            };

            var refreshTokenCookieOptions = new CookieOptions
            {
                Secure = true, // cookie is to be transmitted over https only
                HttpOnly = true,
                Expires = tokens.RefreshTokenExpires,
                SameSite = SameSiteMode.Strict
            };

            // Set access token in cookie
            Response.Cookies.Append("access_token", tokens.AccessToken, accessTokenCookieOptions);
            // Set refresh token in cookie
            Response.Cookies.Append("refresh_token", tokens.RefreshToken, refreshTokenCookieOptions);

            return Ok(response);
        }

        return NotFound(response);
    }
}
