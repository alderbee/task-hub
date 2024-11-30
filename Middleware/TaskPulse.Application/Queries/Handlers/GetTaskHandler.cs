using MediatR;
using TaskPulse.Application.Queries.TaskManagemnet;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.interfaces;

namespace TaskPulse.Application.Queries.Handlers;

public class GetTaskHandler(ITaskRepository taskRepository) : IRequestHandler<GetTaskQuery, List<Domain.Entities.TaskModel>>
{
    public async Task<List<TaskModel>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
       return await taskRepository.GetTask(request.UserId);

    }
}