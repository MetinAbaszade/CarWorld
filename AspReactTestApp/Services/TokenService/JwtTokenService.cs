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
        public string GenerateAccessToken(User user)
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

            // Create the signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create the JWT token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            // Write the JWT token to a string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Set the accessToken cookie
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = token.ValidTo
            };

            return jwt;
        }

        // GenerateRefreshToken method to create a new refresh token
        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(30)
            };

            return refreshToken;
        }
    }
}
