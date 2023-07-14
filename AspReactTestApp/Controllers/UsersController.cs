using AspReactTestApp.DTOs; 
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.Services.UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
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
