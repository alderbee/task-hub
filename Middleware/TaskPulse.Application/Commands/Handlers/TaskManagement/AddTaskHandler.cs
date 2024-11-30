using MediatR;
using TaskPulse.Application.Commands.TaskManagement;
using TaskPulse.Domain.interfaces;

namespace TaskPulse.Application.Handlers.TaskManagement;

public class AddTaskHandler(ITaskRepository taskRepository) : IRequestHandler<AddTaskCommand, bool>
{
    public async Task<bool> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        return await taskRepository.AddTask(request.AddTaskModel);
     
    }
}