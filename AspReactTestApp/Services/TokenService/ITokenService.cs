using AspReactTestApp.Entities.Concrete;
using Newtonsoft.Json.Linq;

namespace AspReactTestApp.Services.TokenService
{
    public interface ITokenService
    {
        public AuthToken GenerateAccessToken(User user);
        public AuthToken GenerateRefreshToken();
    }
}
