using MediatR;
using TaskPulse.Application.Commands.UsersManagement;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Helpers;
using TaskPulse.Domain.interfaces;
using TaskPulse.Domain.interfaces.Helper;

namespace TaskPulse.Application.Handlers.UsersManagement;

public class UserRegistrationHandler(IUserRepository userRepository, ICaptchaVerificationService _captchaVerificationService)
    : IRequestHandler<UserRegistrationCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var isCaptchaValid = await _captchaVerificationService.IsCaptchaValid(request?.Userdata?.captchToken);
        
        if (!isCaptchaValid)
        {
            throw new InvalidOperationException(Constants.ErrorMessages.CaptchaVerfiy);
        }
        
        return await userRepository.UserRegistration(request.Userdata);
    }
    
}