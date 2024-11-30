namespace TaskPulse.Infrastructure.Services;

public interface IEncryptPassword
{
    public string Encrypt(string password);
}