using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.AuthService
{
    public interface IAuthService
    {
        public Task<(ResponseDto, TokenDto)> LogInUser(LoginUserDto request);
        public Task<ResponseDto> LogOutUser();
        public Task<(ResponseDto, TokenDto)> RefreshToken();
        public bool IsUserAuthenticated();
    }
}
