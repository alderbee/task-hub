using MediatR;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Commands.UsersManagement;

public record LoginUserCommand(LoginUser LoginUser) : IRequest<LoginResponse>;