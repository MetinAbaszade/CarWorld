using FluentValidation;
using AspReactTestApp.DTO;

namespace AspReactTestApp.Validations;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    { 
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
