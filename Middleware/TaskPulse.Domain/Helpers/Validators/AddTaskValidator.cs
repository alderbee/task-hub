using FluentValidation;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Domain.Helpers.Validators;

public class AddTaskValidator : AbstractValidator<AddTask>
{
    public AddTaskValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage(Constants.ValidationErrorMessages.UserIdRequired);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(25).WithMessage(Constants.ValidationErrorMessages.TitleRequired);
        RuleFor(x => x.Status).GreaterThan((byte)0).WithMessage(Constants.ValidationErrorMessages.StatusRequired);
        RuleFor(x => x.PriorityId).NotEmpty().GreaterThan((byte)0).WithMessage(Constants.ValidationErrorMessages.StatusRequired);
    }
}