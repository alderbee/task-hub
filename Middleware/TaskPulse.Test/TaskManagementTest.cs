using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskPulse.API.Controllers;
using TaskPulse.Application.Commands.TaskManagement;
using TaskPulse.Application.Queries.TaskManagemnet;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Domain.Helpers;

namespace TaskPulse.Test;

public class TaskManagementTest
{
    
    //GetTask test cases
    
    [Fact]
    public async Task GetTask_ValidUserId_ReturnsOkWithTasks()
    {
        // Arrange
        int userId = 1;
        var mockSender = new Mock<ISender>();
        var expectedTasks = new List<TaskModel>
        {
            new TaskModel { TaskId = 1 },
        };

        mockSender
            .Setup(m => m.Send(It.Is<GetTaskQuery>(q => q.UserId == userId), default))
            .ReturnsAsync(expectedTasks);

        var controller = new TaskManagementController(mockSender.Object, null, null);

        // Act
        var result = await controller.GetTask(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualTasks = Assert.IsType<List<TaskModel>>(okResult.Value); 
        Assert.Equal(expectedTasks.Count, actualTasks.Count); 
        Assert.Equal(expectedTasks, actualTasks);
    }
    
    
    [Fact]
    public async Task GetTask_ValidUserId_NoTask_ReturnsBadRequestWithErrorMessage()
    {
        // Arrange
        int userId = 2;
        var mockSender = new Mock<ISender>();
        var expectedTasks = new List<TaskModel>();

        mockSender
            .Setup(m => m.Send(It.Is<GetTaskQuery>(q => q.UserId == userId), default))
            .ReturnsAsync(expectedTasks);

        var controller = new TaskManagementController(mockSender.Object, null, null);

        // Act
        var result = await controller.GetTask(userId);

        // Assert
        var okResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var actualTasks = Assert.IsType<string>(okResult.Value); 
        Assert.Equal(Constants.ErrorMessages.GetTaskNull, actualTasks);
    }
    
    
    [Fact]
    public async Task GetTask_InvalidUserId_ReturnsBadRequestWithErrorMessage()
    {
        // Arrange
        int userId = 0;
        var mockSender = new Mock<ISender>();
        var expectedTasks = new List<TaskModel>();

        mockSender
            .Setup(m => m.Send(It.Is<GetTaskQuery>(q => q.UserId == userId), default))
            .ReturnsAsync(expectedTasks);

        var controller = new TaskManagementController(mockSender.Object, null, null);

        // Act
        var result = await controller.GetTask(userId);

        // Assert
        var okResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var actualTasks = Assert.IsType<string>(okResult.Value); 
        Assert.Equal(Constants.ErrorMessages.GetTaskLogin, actualTasks);
    }
    
    [Fact]
    public async Task GetTask_ValidUserId_CallsMediatROnce()
    {
        // Arrange
        int userId = 1;
        var mockSender = new Mock<ISender>();
        var expectedTasks = new List<TaskModel>
        {
            new TaskModel { TaskId = 1 },
        };

        mockSender
            .Setup(m => m.Send(It.Is<GetTaskQuery>(q => q.UserId == userId), default))
            .ReturnsAsync(expectedTasks);

        var controller = new TaskManagementController(mockSender.Object, null, null);

        // Act
        await controller.GetTask(userId);

        // Assert
        mockSender.Verify(m => m.Send(It.Is<GetTaskQuery>(q => q.UserId == userId), default), Times.Once);
    }
    
    //Update task
    
    [Fact]
    public async Task UpdateTask_ValidUserData_ReturnsOkWithTrue()
    {
        // Arrange
        var userData = new UpdateTask
        {
            TaskId = 1,
            UserId = 1,
            Title = "Updated Task Title",
            Status = 1
        };

        var mockSender = new Mock<ISender>();
        var mockTaskValidator = new Mock<IValidator<AddTask>>();  
        var mockUpdateTaskValidator = new Mock<IValidator<UpdateTask>>(); 
        
        var validationResult = new ValidationResult(); 
        
        mockUpdateTaskValidator.Setup(v => v.ValidateAsync(userData, default))
            .ReturnsAsync(validationResult);
        
        var expectedResponse = true;
        
        mockSender.Setup(s => s.Send(It.IsAny<UpdateTaskCommand>(), default))
            .ReturnsAsync(expectedResponse);
        
        var controller = new TaskManagementController(mockSender.Object, mockTaskValidator.Object, mockUpdateTaskValidator.Object);

        // Act
        var result = await controller.UpdateTask(userData);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualResponse = Assert.IsType<bool>(okResult.Value);
        Assert.True(actualResponse);
    }
    
    [Fact]
    public async Task UpdateTask_InvalidUserId_ReturnsBadRequest()
    {
        // Arrange
        var userData = new UpdateTask
        {
            TaskId = 1,
            Title = "Updated Task Title",
            Status = 1
        };

        var mockSender = new Mock<ISender>();
        var mockTaskValidator = new Mock<IValidator<AddTask>>();  
        var mockUpdateTaskValidator = new Mock<IValidator<UpdateTask>>(); 
        
        var validationResult = new ValidationResult(new[] { new ValidationFailure("UserId", Constants.ValidationErrorMessages.UserIdRequired) });
        
        mockUpdateTaskValidator.Setup(v => v.ValidateAsync(userData, default))
            .ReturnsAsync(validationResult);
        
        var expectedResponse = true;
        
        mockSender.Setup(s => s.Send(It.IsAny<UpdateTaskCommand>(), default))
            .ReturnsAsync(expectedResponse);
        
        var controller = new TaskManagementController(mockSender.Object, mockTaskValidator.Object, mockUpdateTaskValidator.Object);

        // Act
        var result = await controller.UpdateTask(userData);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var errorMessage = Assert.IsType<string>(badRequestResult.Value);
        Assert.Equal(Constants.ValidationErrorMessages.UserIdRequired, errorMessage);

    }
    
    //Remove task
    
    [Fact]
    public async Task RemoveTask_SenderThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        int taskId = 1;  
        var mockSender = new Mock<ISender>();
        
        mockSender.Setup(m => m.Send(It.Is<RemoveTaskCommand>(q => q.TaskId == taskId), default))
            .ThrowsAsync(new Exception(Constants.ErrorMessages.GeneralException));

        var controller = new TaskManagementController(mockSender.Object, null, null);
    
        // Act
        Exception exception = await Assert.ThrowsAsync<Exception>(() => controller.RemoveTask(taskId));

        // Assert
        Assert.Equal(Constants.ErrorMessages.GeneralException, exception.Message);
    }
}