using AspReactTestApp.Dto;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.AuthService
{
    public interface IAuthService
    {
        public Task<(ResponseDto, TokenDto)> LoginUser(LoginUserDto request);
        public Task<(ResponseDto, TokenDto)> RefreshToken();
        public bool IsUserAuthenticated();
    }
}
