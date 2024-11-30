using MediatR;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Application.Commands.TaskManagement;

public record AddTaskCommand(AddTask AddTaskModel): IRequest<bool>;