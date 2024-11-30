namespace TaskPulse.Infrastructure.Services;

public interface ITokenCreator
{
    Task<string> CreateToken(string username);
}