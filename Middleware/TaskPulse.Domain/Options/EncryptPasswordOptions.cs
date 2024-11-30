namespace TaskPulse.Domain.Options;

public class EncryptPasswordOptions
{
      
    public const string SectionName = "AppSettings";
    
    public string Token { get; set; }
    public string PasswordHashKey { get; set; }
}