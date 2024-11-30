using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskPulse.Application.Commands.UsersManagement;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Domain.Helpers;

namespace TaskPulse.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserManagementController : ControllerBase
{
    private readonly ISender sender;
    private readonly IValidator<UserRegistration> registrationValidator;
    
    public UserManagementController(ISender sender,IValidator<UserRegistration> registrationValidator)
    {
        this.sender = sender;
        this.registrationValidator = registrationValidator;
    }
    [HttpPost]
    [ApiVersion("1.0")]
    [Route("Registration")]
    public async Task<ActionResult<LoginResponse>> UserRegistration([FromBody] UserRegistration userData)
    {
        
        var validationResult = await registrationValidator.ValidateAsync(userData);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault());
        }
        
        return Ok(await sender.Send(new UserRegistrationCommand(userData)));
    }

    [HttpPost]
    [ApiVersion("1.0")]
    [Route("Login")]
    public async Task<ActionResult<LoginResponse>> UserLogin([FromBody] LoginUser loginUser)
    {
        if ((loginUser.password is null) || (loginUser.username is null))
        {
            return BadRequest(Constants.ErrorMessages.UserLogin);
        }
        return Ok(await sender.Send(new LoginUserCommand(loginUser)));
    }
    
}