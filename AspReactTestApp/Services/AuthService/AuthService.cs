using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using AspReactTestApp.CustomExceptions;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Services.FileService;
using AspReactTestApp.Services.EmailService;
using AspReactTestApp.Services.TokenService;
using AspReactTestApp.Validations;

namespace AspReactTestApp.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserDal userDal, 
                           IEmailService emailService, 
                           ITokenService tokenService, 
                           IFileService fileService, 
                           IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _emailService = emailService;
            _tokenService = tokenService;
            _fileService = fileService;
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
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await SetRefreshToken(refreshToken, user);

            return new AuthResponseDto()
            {
                IsSuccessfull = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                TokenExpires = refreshToken.Expires
            };
        }

        // RegisterUser method for user registration
        public async Task<AuthResponseDto> RegisterUser(RegisterUserDto request)
        {
            var registerValidator = new RegisterUserDtoValidator();
            var validationResult = registerValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = new Dictionary<string, string>();

                foreach (var error in validationResult.Errors)
                {
                    errors[error.PropertyName] = error.ErrorMessage;
                }

                return new AuthResponseDto
                {
                    Message = "There are Validation errors",
                    Errors = errors
                };
            }

            // Create password hash and salt
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var profileImageUrl = string.Empty;
            if (request.ProfileImage == null)
            {
                profileImageUrl = _fileService.GetDefaultProfileImageUrl();
            }
            else
            {
                profileImageUrl = await _fileService.SaveProfileImage(request.ProfileImage);
            }

            // Create user object
            var user = new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.UserName,
                ProfileImageUrl = profileImageUrl,
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
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            var user = await _userDal.Get(user => user.RefreshToken == refreshToken);

            if (user == null)
            {
                throw new InvalidTokenException("Invalid RefreshToken!");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                throw new ExpiredTokenException("Token has expired!");
            }

            // Generating new Access and Refresh Token
            string newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            await SetRefreshToken(newRefreshToken, user);

            return new AuthResponseDto
            {
                IsSuccessfull = true,
                Message = "Token Successfully refreshed!",
                AccessToken = newAccessToken,
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
            }
        }

        // SendVerificationCode method to send a verification code to a user's email
        public AuthResponseDto SendVerificationCode([FromBody] string recipientEmail)
        {
            try
            {
                if (!_emailService.CanRequestVerificationCode(recipientEmail))
                {
                    Dictionary<string, string> errors = new();
                    var errorMessage = "You have requested too many verification code, please try again later";
                    errors["email"] = errorMessage;
                    return new AuthResponseDto
                    {
                        IsSuccessfull = false,
                        Message = errorMessage, 
                        Errors = errors
                    };
                }

                var verificationCode = _emailService.GenerateVerificationCode();
                var verificationEmailBody = _emailService.GenerateVerificationEmail(verificationCode);
                var verificationEmailSubject = "Verification Code for CarUniverse Account (Expires in 2 Minutes)";

                _emailService.SendEmail(recipientEmail, verificationEmailSubject, verificationEmailBody);
                _emailService.StoreVerificationCodeInCache(recipientEmail, verificationCode);


                return new AuthResponseDto
                {
                    IsSuccessfull = true,
                    Message = "Verification sent successfully"
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

        // CheckVerificationCode method to validate the user-provided verification code
        public AuthResponseDto CheckVerificationCode(CheckVerificationCodeDto verificationCodeDto)
        {
            try
            {
                var result = _emailService.CheckVerificationCode(verificationCodeDto);
                if (!result.IsSuccessful)
                {
                    return new AuthResponseDto
                    {
                        IsSuccessfull = false,
                        Message = result.Message,
                        Errors = result.Errors
                    };
                }

                return new AuthResponseDto
                {
                    IsSuccessfull = true,
                    Message = result.Message
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
    }
}


