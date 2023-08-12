using AspReactTestApp.Dto;
using AspReactTestApp.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AspReactTestApp.Services.TokenService
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GenerateAccessToken method to generate a JWT access token
        public AuthToken GenerateAccessToken(User user)
        {
            // Create claims for the JWT token
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Create the signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Appsettings:Token").Value));

            var accessTokenExpireTimeinMunutes = _configuration.GetSection("DefaultValues:AccessTokenExpireTimeinMinutes").Value;
            var accessTokenExpireTimeinMunutesInt = int.Parse(accessTokenExpireTimeinMunutes);
            // Create the signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var accessTokenExpireTime = DateTime.Now.AddMinutes(accessTokenExpireTimeinMunutesInt);

            // Create the JWT token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: accessTokenExpireTime,
                signingCredentials: creds);

            // Write the JWT token to a string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthToken()
            {
                Token = jwt,
                Expires = accessTokenExpireTime
            };
        }

        // GenerateRefreshToken method to create a new refresh token
        public AuthToken GenerateRefreshToken()
        {
            var refreshTokenExpireTimeinDays = _configuration.GetSection("DefaultValues:RefreshTokenExpireTimeinDays").Value;
            var refreshTokenExpireTimeinDaysInt = int.Parse(refreshTokenExpireTimeinDays);

            var refreshToken = new AuthToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(refreshTokenExpireTimeinDaysInt)
            };

            return refreshToken;
        }
    }
}
