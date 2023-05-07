using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.TokenService
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public RefreshToken GenerateRefreshToken();
    }
}
