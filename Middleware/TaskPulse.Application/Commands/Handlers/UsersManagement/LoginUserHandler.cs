using MediatR;
using TaskPulse.Application.Commands.UsersManagement;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Helpers;
using TaskPulse.Domain.interfaces;
using TaskPulse.Domain.interfaces.Helper;

namespace TaskPulse.Application.Handlers.UsersManagement;

public class LoginUserHandler(IUserRepository userRepository, ICaptchaVerificationService _captchaVerificationService) : IRequestHandler<LoginUserCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var isCaptchaValid = await _captchaVerificationService.IsCaptchaValid(request?.LoginUser?.captchToken);
        
        if (!isCaptchaValid)
        {
            throw new InvalidOperationException(Constants.ErrorMessages.CaptchaVerfiy);
        }

        return await userRepository.LoginUser(request.LoginUser);
    }
}