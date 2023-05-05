using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.AuthService
{
    public interface IAuthSevice
    {
        public Task<AuthResponseDto> LoginUser(LoginUserDto request);
        public Task<AuthResponseDto> RegisterUser(RegisterUserDto request);
        public Task<AuthResponseDto> RefreshToken();
        public bool SendVerificationCode(string email);
    }
}
