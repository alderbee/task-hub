using MediatR;
using TaskPulse.Application.Commands.TaskManagement;
using TaskPulse.Domain.interfaces;

namespace TaskPulse.Application.Handlers.TaskManagement;

public class UpdateTaskHandler(ITaskRepository taskRepository) : IRequestHandler<UpdateTaskCommand, bool>
{
    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        return await taskRepository.UpdateTask(request.TaskModel);
    }
}