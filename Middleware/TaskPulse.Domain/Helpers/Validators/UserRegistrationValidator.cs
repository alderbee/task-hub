using FluentValidation;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Domain.Helpers.Validators;

public class UserRegistrationValidator : AbstractValidator<UserRegistration>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.username).NotEmpty().WithMessage(Constants.ValidationErrorMessages.UserNameRequired);
        RuleFor(x => x.email).NotEmpty().EmailAddress().WithMessage(Constants.ValidationErrorMessages.EmailIdRequired);
        RuleFor(x => x.password).NotEmpty().WithMessage(Constants.ValidationErrorMessages.PasswordRequired);
        RuleFor(x => x.captchToken).NotEmpty().WithMessage(Constants.ValidationErrorMessages.CaptchaRequired);
    }
}