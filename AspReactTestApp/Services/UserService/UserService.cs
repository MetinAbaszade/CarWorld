using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.DTO;
using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Services.FileService;
using AspReactTestApp.Validations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AspReactTestApp.Services.UserService;

public class UserService(IUserRepository userDal, 
                         IFileService fileService, 
                         IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IUserRepository _userDal = userDal;
    private readonly IFileService _fileService = fileService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    // RegisterUser method for user registration
    public async Task<ResponseDTO> RegisterUser(RegisterUserDTO request)
    {
        var registerValidator = new RegisterUserDTOValidator();
        var validationResult = registerValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = new Dictionary<string, string>();

            foreach (var error in validationResult.Errors)
            {
                errors[error.PropertyName] = error.ErrorMessage;
            }

            return new ResponseDTO
            {
                Message = "There are Validation errors",
                Errors = errors
            };
        }

        // Create password hash and salt
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        string profileImageUrl;
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
            Email = request.Email,
            Number = "My Number",
            Surname = request.Surname,
            UserName = request.UserName,
            ProfileImageUrl = profileImageUrl,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = "User"
        };

        // Add user to the database
        await _userDal.Add(user);

        return new ResponseDTO()
        {
            IsSuccessfull = true, 
            Message = "User has successfully been created"
        };
    }

    // CreatePasswordHash method to generate a password hash and salt
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        // If we don't use Salt, then hashed password will be the same always.
        // That's why we add different salt each time to the password and hash them as combined.
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public async Task<bool> CheckUserExists(string userName)
    {
        var user = await _userDal.Get(user => user.UserName == userName);
        return user != null;
    }

    public string GetUserRole()
    {
        string result;
        result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        return result;
    }
}
