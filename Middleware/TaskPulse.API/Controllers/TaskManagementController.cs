using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TaskPulse.Application.Commands.TaskManagement;
using TaskPulse.Application.Queries.TaskManagemnet;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Domain.Helpers;
using TaskPulse.Domain.Helpers.Validators;

namespace TaskPulse.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TaskManagementController : Controller
{
    
    private readonly IValidator<AddTask> taskValidator;
    private readonly IValidator<UpdateTask> updateTaskValidator;
    private readonly ISender sender;

    public TaskManagementController(ISender sender, IValidator<AddTask> taskValidator, IValidator<UpdateTask> updateTaskValidator)
    {
        this.sender = sender;
        this.taskValidator = taskValidator;
        this.updateTaskValidator = updateTaskValidator;
    }
    
    
    [Authorize]
    [HttpPost]
    [ApiVersion("1.0")]
    [Route("AddTask")]
    public async Task<ActionResult<bool>> AddTask([FromBody] AddTask userData)
    {
        var validationResult = await taskValidator.ValidateAsync(userData);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
        }
        
        return Ok(await sender.Send(new AddTaskCommand(userData)));
    }
    
    [Authorize]
    [HttpGet]
    [ApiVersion("1.0")]
    [Route("GetTask")]
    public async Task<ActionResult<List<TaskModel>>> GetTask([FromQuery]int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(Constants.ErrorMessages.GetTaskLogin);
        }

        var tasks = await sender.Send(new GetTaskQuery(userId));

        if (tasks.Count == 0)
        {
            return NotFound(Constants.ErrorMessages.GetTaskNull);
        }

        return Ok(tasks);
        
    }
    
    [Authorize]
    [HttpPut]
    [ApiVersion("1.0")]
    [Route("UpdateTask")]
    public async Task<ActionResult<bool>> UpdateTask([FromBody] UpdateTask userData)
    {
        
        var validationResult = await updateTaskValidator.ValidateAsync(userData);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

        }
        
        return Ok(await sender.Send(new UpdateTaskCommand(userData)));
    }
    
    [Authorize]
    [HttpDelete]
    [ApiVersion("1.0")]
    [Route("RemoveTask")]
    public async Task<ActionResult<bool>> RemoveTask([FromQuery]int taskId)
    {
        if (taskId <= 0)
        {
            return BadRequest(Constants.ErrorMessages.GetTaskLogin);
        }
        return Ok(await sender.Send(new RemoveTaskCommand(taskId)));
    }

}