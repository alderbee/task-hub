using MediatR;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Queries.TaskManagemnet;

public record GetTaskQuery(int UserId): IRequest<List<TaskModel>>;