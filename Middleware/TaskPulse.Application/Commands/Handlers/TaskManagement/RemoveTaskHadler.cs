using MediatR;
using TaskPulse.Application.Commands.TaskManagement;
using TaskPulse.Domain.interfaces;

namespace TaskPulse.Application.Handlers.TaskManagement;

public class RemoveTaskHadler(ITaskRepository taskRepository): IRequestHandler<RemoveTaskCommand, bool>
{
    public async Task<bool> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        return await taskRepository.RemoveTask(request.TaskId);
    }
}