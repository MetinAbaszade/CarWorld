using AspReactTestApp.DTO; 
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.Services.UserService;

namespace AspReactTestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<ActionResult<ResponseDTO>> Register([FromForm] RegisterUserDTO request)
    {
        var user = await _userService.RegisterUser(request);
        return Ok(user);
    }
}
