using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Domain.interfaces;

public interface ITaskRepository
{
    public Task<bool> AddTask(AddTask addTaskModel);
    public Task<List<TaskModel>> GetTask(int userId);
    public Task<bool> RemoveTask(int taskId);
    public Task<bool> UpdateTask(UpdateTask taskModel);
}