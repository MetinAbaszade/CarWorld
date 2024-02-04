using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.TokenService;

public interface ITokenService
{
    public AuthToken GenerateAccessToken(User user);
    public AuthToken GenerateRefreshToken();
}
