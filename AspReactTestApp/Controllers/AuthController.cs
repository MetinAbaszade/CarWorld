using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Services.AuthService;
using AspReactTestApp.Services.UserService;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthSevice _authSevice;
        private readonly IUserService _userService;

        public AuthController(IAuthSevice authSevice, IUserService userService)
        {
            _authSevice = authSevice;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginUserDto request)
        {
            var response = await _authSevice.LoginUser(request);
            if (response.IsSuccessfull)
                return Ok(response);

            return NotFound(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> RegisterUser(RegisterUserDto request)
        {
            var user = await _authSevice.RegisterUser(request);
            return Ok(user);
        }

        [HttpPost("checkuserexists")]
        public async Task<ActionResult<bool>> CheckUserExists(LoginUserDto request)
        {
            var result = await _userService.CheckUserExists(request.UserName);
            if (result)
                return Ok(result);
            return NotFound(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var response = await _authSevice.RefreshToken();

            if (response.IsSuccessfull)
                return Ok(response);

            return BadRequest(response);
        }

    }
}
