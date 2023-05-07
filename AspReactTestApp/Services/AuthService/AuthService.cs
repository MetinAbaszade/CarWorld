using AspReactTestApp.CustomExceptions;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using AspReactTestApp.Services.EmailService;

namespace AspReactTestApp.Services.AuthService
{
    // AuthService class that implements the IAuthService interface
    public class AuthService : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // AuthService constructor
        public AuthService(IUserDal userDal, 
                           IMemoryCache memoryCache, 
                           IEmailService emailService, 
                           IConfiguration configuration, 
                           IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _memoryCache = memoryCache;
            _emailService = emailService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        // LoginUser method for user authentication
        public async Task<AuthResponseDto> LoginUser(LoginUserDto request)
        {
            // Retrieve user if exists
            var user = await _userDal.Get(user => user.UserName == request.UserName);

            if (user == null)
            {
                return new AuthResponseDto("User Not Found!");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResponseDto("Password is Wrong!");
            }

            // Create access token and refresh token
            var token = CreateToken(user);
            var refreshToken = GenerateRefreshToken();
            await SetRefreshToken(refreshToken, user);

            return new AuthResponseDto()
            {
                IsSuccessfull = true,
                AccessToken = token,
                RefreshToken = refreshToken.Token,
                TokenExpires = refreshToken.Expires
            };
        }

        // RegisterUser method for user registration
        public async Task<AuthResponseDto> RegisterUser(RegisterUserDto request)
        {
            // Create password hash and salt
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Create user object
            var user = new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.UserName,
                // ProfileImageUrl = request.ProfileImageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User"
            };

            // Add user to the database
            await _userDal.Add(user);

            return new AuthResponseDto()
            {
                IsSuccessfull = true
            };
        }

        // CreateToken method to generate a JWT access token
        private string CreateToken(User user)
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

            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("accessToken", jwt, cookieOptions);

            return jwt;
        }

        // GenerateRefreshToken method to create a new refresh token
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(30)
            };

            return refreshToken;
        }

        // SetRefreshToken method to store a refresh token in a cookie and update the user record
        private async Task SetRefreshToken(RefreshToken refreshToken, User user)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.RefreshToken = refreshToken.Token;
            user.TokenExpires = refreshToken.Expires;

            await _userDal.Update(user);
        }

        // RefreshToken method to refresh an existing JWT access token
        public async Task<AuthResponseDto> RefreshToken()
        {
            var Users = await _userDal.GetList();
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            var user = Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null)
            {
                throw new InvalidTokenException("Invalid RefreshToken!");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                throw new ExpiredTokenException("Token has expired!");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken, user);

            return new AuthResponseDto
            {
                IsSuccessfull = true,
                Message = "Token Successfully refreshed!",
                AccessToken = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }

        // CreatePasswordHash method to generate a password hash and salt
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // If we don't use Salt, then hashed password will be the same always.
            // That's why we add different salt each time to the password and hash them as combined.
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // VerifyPasswordHash method to check if a given password matches the stored hash and salt
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
                // SequenceEqual vs Equals
            }
        }

        // GenerateVerificationCode method to create a new verification code
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 1000000).ToString();
        }

        // SendVerificationCode method to send a verification code to a user's email
        public AuthResponseDto SendVerificationCode(string recipientEmail)
        {
            if (!CanRequestVerificationCode(recipientEmail))
            {
                return new AuthResponseDto
                {
                    IsSuccessfull = false,
                    Message = "You can request a new verification code only once per minute."
                };
            }

            try
            {
                var subject = "Verification Code for CarUniverse Account (Expires in 2 Minutes)";

                var verificationCode = GenerateVerificationCode();
                StoreVerificationCodeInCache(recipientEmail, verificationCode);

                var body = @$"<div style=""font-size: 20px;"">
Thank you for choosing CarUniverse as your preferred platform for all your automotive needs. As part of our security measures, we require all users to verify their account before gaining full access to our services.<br><br>

Your verification code is: <b>{{verificationCode}}</b><br><br>

Please enter this code in the designated field on the CarUniverse app or website to complete the verification process. Please note that this code is only valid for the next 2 minutes. After that, it will expire and you will need to request a new code.<br><br>

If you did not request this verification code, please disregard this message.<br><br>

If you have any questions or concerns, please do not hesitate to contact our customer support team for assistance. We are available 24/7 to assist you.<br>

Thank you for choosing CarUniverse.<br><br>

Best regards,<br>
The CarUniverse Team

</div>";

                _emailService.SendEmail(recipientEmail, subject, body);

                return new AuthResponseDto
                {
                    IsSuccessfull = true,
                    Message = "Verification Code Sent Successfully"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Message = ex.Message
                };
            }
        }

        // StoreVerificationCodeInCache method to store the generated verification code in the cache
        private void StoreVerificationCodeInCache(string email, string verificationCode)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            _memoryCache.Set(email, verificationCode, cacheEntryOptions);
        }

        // CheckVerificationCode method to validate the user-provided verification code
        public AuthResponseDto CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto)
        {
            var keyExists = _memoryCache.TryGetValue(verificationCodeDto.Email, out string storedVerificationCode);

            if (!keyExists)
            {
                return new AuthResponseDto
                {
                    Message = "Please request code again"
                };
            }

            if (storedVerificationCode == verificationCodeDto.VerificationCode)
            {
                return new AuthResponseDto
                {
                    IsSuccessfull = true,
                    Message = "Verification code is correct"
                };
            }

            return new AuthResponseDto
            {
                Message = "Verification code is not correct"
            };
        }

        // CanRequestVerificationCode method to determine whether the user can request a new verification code
        private bool CanRequestVerificationCode(string email)
        {
            if (!_memoryCache.TryGetValue($"{email}_lastRequest", out DateTime lastRequestTimestamp))
            {
                // No previous request, allow the current request
                _memoryCache.Set($"{email}_lastRequest", DateTime.UtcNow);
                return true;
            }

            var timeSinceLastRequest = DateTime.UtcNow - lastRequestTimestamp;

            if (timeSinceLastRequest.TotalMinutes >= 1)
            {
                // Last request was more than a minute ago, update the timestamp and allow the current request
                _memoryCache.Set($"{email}_lastRequest", DateTime.UtcNow);
                return true;
            }

            // Last request was within the last minute, disallow the current request
            return false;
        }
    }
}


