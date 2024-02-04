using AspReactTestApp.DTO;

namespace AspReactTestApp.Services.AuthService;

public interface IAuthService
{
    public Task<(ResponseDTO, TokenDTO)> LogInUser(LoginUserDTO request);
    public Task<ResponseDTO> LogOutUser();
    public Task<(ResponseDTO, TokenDTO)> RefreshToken();
    public bool IsUserAuthenticated();
}
