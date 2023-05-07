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
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginUserDto request)
        {
            var response = await _authService.LoginUser(request);
            if (response.IsSuccessfull)
                return Ok(response);

            return NotFound(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> RegisterUser(RegisterUserDto request)
        {
            var user = await _authService.RegisterUser(request);
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
        public async Task<ActionResult<AuthResponseDto>> RefreshToken()
        {
            var response = await _authService.RefreshToken();

            if (response.IsSuccessfull)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("sendverificationcode")]
        public ActionResult<AuthResponseDto> SendVerificationCode([FromBody]string recipientEmail)
        {
            var result = _authService.SendVerificationCode(recipientEmail);
            if (result.IsSuccessfull)
                return Ok(result);
            return NotFound(result);
        }

        [HttpPost("checkverificationcode")]
        public ActionResult<AuthResponseDto> CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto)
        {
            var result = _authService.CheckVerificationCode(verificationCodeDto);
            if (result.IsSuccessfull)
                return Ok(result);
            return NotFound(result);
        }

    }
}
