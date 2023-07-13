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
using System.Linq;
using FluentValidation;
using AspReactTestApp.Dto;

namespace AspReactTestApp.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userDal;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userDal,
                           ITokenService tokenService,
                           IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        // LoginUser method for user authentication
        public async Task<(ResponseDto, TokenDto)> LoginUser(LoginUserDto request)
        {

            var loginValidator = new LoginUserDtoValidator();
            var validationResult = loginValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = new Dictionary<string, string>();

                foreach (var error in validationResult.Errors)
                {
                    errors[error.PropertyName] = error.ErrorMessage;
                }

                return (new ResponseDto
                {
                    Message = "There are Validation errors",
                    Errors = errors
                }, new TokenDto());
            }


            // Retrieve user if exists
            var user = await _userDal.Get(user => user.UserName == request.UserName);

            if (user == null)
            {
                var errors = new Dictionary<string, string>();
                errors["userName"] = "User Not Found!";

                return (new ResponseDto
                {
                    Message = "User Not Found!",
                    Errors = errors
                }, new TokenDto());
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                var errors = new Dictionary<string, string>();
                errors["password"] = "Password is false!";

                return (new ResponseDto
                {
                    Message = "Password is false!",
                    Errors = errors
                }, new TokenDto());
            }

            // Create access token and refresh token
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken.Token;
            user.TokenExpires = refreshToken.Expires;
            await _userDal.Update(user);

            return (
                new ResponseDto()
                {
                    IsSuccessfull = true,
                    Message = "User has successfully been logged in"

                },
                new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                    TokenExpires = refreshToken.Expires
                }
             );
        }

        // RefreshToken method to refresh an existing JWT access token
        public async Task<(ResponseDto, TokenDto)> RefreshToken()
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
            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken.Token;
            user.TokenExpires = newRefreshToken.Expires;
            await _userDal.Update(user);

            return (
                new ResponseDto
                {
                    IsSuccessfull = true,
                    Message = "Token Successfully refreshed!",
                },
                new TokenDto()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken.Token,
                    TokenExpires = newRefreshToken.Expires
                }
            );
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

    }
}


