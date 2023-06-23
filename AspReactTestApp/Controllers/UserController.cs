using AspReactTestApp.DTOs;
using AspReactTestApp.Services.AuthService;
using AspReactTestApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> Register([FromForm] RegisterUserDto request)
        {
            var user = await _userService.RegisterUser(request);
            return Ok(user);
        }
    }
}
