using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.AuthService
{
    public interface IAuthSevice
    {
        public Task<User> RegisterUser(UserDto request);
        public Task<AuthResponseDto> LoginUser(UserDto request);
        public Task<AuthResponseDto> RefreshToken();
    }
}
