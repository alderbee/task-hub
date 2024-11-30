using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Domain.interfaces;
using TaskPulse.Infrastructure.Data;

namespace TaskPulse.Infrastructure.Repositories;

public class TaskRepository(ApplicationDbContext context,IMapper mapper) : ITaskRepository
{
    public async Task<bool> AddTask(AddTask addTaskModel)
    {
            TaskModel task = mapper.Map<TaskModel>(addTaskModel);
            context.Add(task);
            var data= await context.SaveChangesAsync(); 
            
            return data>0?true:false;
    }

    public async Task<List<TaskModel>> GetTask(int userId)
    {
            var data = await context.Task.Where(data => data.UserId == userId).AsNoTracking().ToListAsync();
            return data;
    }

    public async Task<bool> RemoveTask(int taskId)
    {
            context.Task.Where(data => data.TaskId == taskId).ExecuteDelete();
            var data =await context.SaveChangesAsync();
            return data>0?true:false;
    }

    public async Task<bool> UpdateTask(UpdateTask taskModel)
    {
            var rowsAffected = await context.Task
                .Where(t => t.TaskId == taskModel.TaskId)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(p => p.Title, taskModel.Title)
                    .SetProperty(p => p.PriorityId, taskModel.PriorityId)
                    .SetProperty(p => p.Status, taskModel.Status)
                    .SetProperty(p => p.StartDate, taskModel.StartDate)
                    .SetProperty(p => p.EndDate, taskModel.EndDate)
                    .SetProperty(p => p.Content, taskModel.Content)
                    .SetProperty(p => p.UpdatedAt, DateTime.Now)
                );

            return rowsAffected > 0 ? true : false;
    }

}