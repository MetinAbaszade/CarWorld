using System.Text;
using AspReactTestApp.DTO;
using AspReactTestApp.Validations;
using System.Security.Cryptography;
using AspReactTestApp.CustomExceptions;
using AspReactTestApp.Services.TokenService;
using AspReactTestApp.Data.DataAccess.Abstract;

namespace AspReactTestApp.Services.AuthService;

public class AuthService(IUserRepository userDal,
                         ITokenService tokenService,
                         IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly IUserRepository _userDal = userDal;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    // LoginUser method for user authentication
    public async Task<(ResponseDTO, TokenDTO)> LogInUser(LoginUserDTO request)
    {
        var loginValidator = new LoginUserDTOValidator();
        var validationResult = loginValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = new Dictionary<string, string>();

            foreach (var error in validationResult.Errors)
            {
                errors[error.PropertyName] = error.ErrorMessage;
            }

            return (new ResponseDTO
            {
                Message = "There are Validation errors",
                Errors = errors
            }, new TokenDTO());
        }

        // Retrieve user if exists
        var user = await _userDal.Get(user => user.UserName == request.UserName);
        if (user == null)
        {
            var errors = new Dictionary<string, string>
            {
                ["userName"] = "User Not Found!"
            };

            return (new ResponseDTO
            {
                Message = "User Not Found!",
                Errors = errors
            }, new TokenDTO());
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            var errors = new Dictionary<string, string>
            {
                ["password"] = "Password is false!"
            };

            return (new ResponseDTO
            {
                Message = "Password is false!",
                Errors = errors
            }, new TokenDTO());
        }

        // Create access token and refresh token
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken.Token;
        user.TokenExpires = refreshToken.Expires;
        await _userDal.Update(user);

        return (
            new ResponseDTO()
            {
                IsSuccessfull = true,
                Message = "User has successfully been logged in"
            },
            new TokenDTO()
            {
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken.Token,
                AccessTokenExpires = accessToken.Expires,
                RefreshTokenExpires = refreshToken.Expires
            }
         );
    }

    public async Task<ResponseDTO> LogOutUser()
    {
        // Retrieve refresh token from the HTTP context
        var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refresh_token"];

        // Find the user with the given refresh token
        var user = await _userDal.Get(user => user.RefreshToken == refreshToken);

        if (user == null)
        {
            return new ResponseDTO
            {
                IsSuccessfull = false,
                Message = "User not found!"
            };
        }

        // Invalidate the refresh token and set the expiration time to now
        user.RefreshToken = null;
        user.TokenExpires = DateTime.Now;

        // Update the user in the database
        await _userDal.Update(user);

        // Clear the access_token and refreshToken cookies
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("refresh_token");

        return new ResponseDTO
        {
            IsSuccessfull = true,
            Message = "User has been successfully logged out"
        };
    }

    // RefreshToken method to refresh an existing JWT access token
    public async Task<(ResponseDTO, TokenDTO)> RefreshToken()
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
            new ResponseDTO
            {
                IsSuccessfull = true,
                Message = "Token Successfully refreshed!",
            },
            new TokenDTO()
            {
                AccessToken = newAccessToken.Token,
                RefreshToken = newRefreshToken.Token,
                AccessTokenExpires = newAccessToken.Expires,
                RefreshTokenExpires = newRefreshToken.Expires
            }
        );
    }

    public bool IsUserAuthenticated()
    {
        var user = _httpContextAccessor.HttpContext.User;
        return user?.Identity?.IsAuthenticated ?? false;
    }

    // VerifyPasswordHash method to check if a given password matches the stored hash and salt
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}


