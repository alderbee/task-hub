using FluentValidation;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Domain.Helpers.Validators;

public class UpdateTaskValidator :AbstractValidator<UpdateTask>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage(Constants.ValidationErrorMessages.UserIdRequired);
        RuleFor(x => x.TaskId).NotEmpty().GreaterThan(0).WithMessage(Constants.ValidationErrorMessages.TaskIdRequired);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(25).WithMessage(Constants.ValidationErrorMessages.TitleRequired);
        RuleFor(x => x.Status).NotEmpty().WithMessage(Constants.ValidationErrorMessages.StatusRequired);
    }
}