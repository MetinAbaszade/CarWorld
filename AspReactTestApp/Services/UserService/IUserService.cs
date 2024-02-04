using AspReactTestApp.DTO;

namespace AspReactTestApp.Services.UserService;

public interface IUserService
{
    public Task<bool> CheckUserExists(string userName);
    public Task<ResponseDTO> RegisterUser(RegisterUserDTO registerUserDto);
}
