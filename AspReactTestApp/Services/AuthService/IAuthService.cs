using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AuthResponseDto> LoginUser(LoginUserDto request);
        public Task<AuthResponseDto> RegisterUser(RegisterUserDto request);
        public Task<AuthResponseDto> RefreshToken();
        public AuthResponseDto SendVerificationCode(string email);
        public AuthResponseDto CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto);
    }
}
