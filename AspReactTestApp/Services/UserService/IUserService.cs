using AspReactTestApp.DTOs;

namespace AspReactTestApp.Services.UserService
{
    public interface IUserService
    {
        public Task<bool> CheckUserExists(string userName);
        public Task<ResponseDto> RegisterUser(RegisterUserDto registerUserDto);
    }
}
