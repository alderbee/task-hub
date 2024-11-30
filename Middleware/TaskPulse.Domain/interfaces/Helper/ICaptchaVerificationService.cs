namespace TaskPulse.Domain.interfaces.Helper;

public interface ICaptchaVerificationService
{
    Task<bool> IsCaptchaValid(string token);
}