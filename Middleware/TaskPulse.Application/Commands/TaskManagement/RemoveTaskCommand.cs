using MediatR;

namespace TaskPulse.Application.Commands.TaskManagement;

public record RemoveTaskCommand(int TaskId) : IRequest<bool>;