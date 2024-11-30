namespace TaskPulse.Domain.Entities;

public class LoginResponse
{
    public LoginResponse()
    {
    }
    public int? userId { get; set; }

    public string token { get; set; }
}