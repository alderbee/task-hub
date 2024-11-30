using MediatR;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Application.Commands.UsersManagement;

public record UserRegistrationCommand(UserRegistration Userdata): IRequest<LoginResponse>;