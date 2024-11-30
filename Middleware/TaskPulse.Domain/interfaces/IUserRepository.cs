using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Domain.interfaces;

public interface IUserRepository
{
    Task<LoginResponse> UserRegistration(UserRegistration entity);
    Task<LoginResponse> LoginUser(LoginUser entity);
}