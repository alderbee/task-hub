namespace TaskPulse.Domain.Options;

public class CaptchaOptions
{
    
    public const string SectionName = "Captcha";
    
    public string ClientKey { get; set; }
    public string ServerKey { get; set; }
    public string VerificationUrl { get; set; }
}
