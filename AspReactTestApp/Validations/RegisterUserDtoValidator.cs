using FluentValidation;
using AspReactTestApp.DTO;

namespace AspReactTestApp.Validations;

public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        RuleFor(x => x.RetypePassword)
            .NotEmpty().WithMessage("Retype password is required")
            .Equal(x => x.Password).WithMessage("Passwords must match");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email");
    }
}
