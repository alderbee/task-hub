using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskPulse.API.Controllers;
using TaskPulse.Application.Commands.UsersManagement;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Domain.Helpers;

namespace TaskPulse.Test
{
    public class UserManagementTest
    {
        
        //Registration test cases
        
        [Fact]
        public async Task UserRegistration_ValidUserData_ReturnsOkWithLoginResponse()
        {
            // Arrange
            var userData = new UserRegistration
            {
                username = "testuser",
                email = "testuser@example.com",
                password = "SecurePassword123",
                captchToken = "validCaptchaToken"
            };
    
            var mockSender = new Mock<ISender>();
            var expectedResponse = new LoginResponse { token = "validToken" }; // Assuming a valid login response

            mockSender
                .Setup(s => s.Send(It.IsAny<UserRegistrationCommand>(), default))
                .ReturnsAsync(expectedResponse);

            var mockValidator = new Mock<IValidator<UserRegistration>>();
            mockValidator.Setup(v => v.ValidateAsync(userData, default))
                .ReturnsAsync(new ValidationResult());

            var controller = new UserManagementController(mockSender.Object, mockValidator.Object);

            // Act
            var result = await controller.UserRegistration(userData);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualResponse = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal(expectedResponse.token, actualResponse.token);
        }
        
        [Fact]
        public async Task UserRegistration_CommandThrowsException_ReturnsExceptionMessage()
        {
            // Arrange
            var userData = new UserRegistration
            {
                username = "testuser",
                email = "testuser@example.com",
                password = "SecurePassword123",
                captchToken = "validCaptchaToken"
            };

            var mockSender = new Mock<ISender>();
            var mockValidator = new Mock<IValidator<UserRegistration>>();
            
            mockValidator.Setup(v => v.ValidateAsync(userData, default))
                .ReturnsAsync(new ValidationResult()); 

            mockSender.Setup(s => s.Send(It.IsAny<UserRegistrationCommand>(), default))
                .ThrowsAsync(new Exception("An error occurred while registering the users"));

            var controller = new UserManagementController(mockSender.Object, mockValidator.Object);

            // Act
            Exception exception = await Assert.ThrowsAsync<Exception>(() => controller.UserRegistration(userData));

            // Assert
            Assert.Equal("An error occurred while registering the users", exception.Message);
        }
        
        [Fact]
        public async Task UserRegistration_InvalidUserData_ReturnsBadRequestWithError()
        {
            // Arrange
            var userData = new UserRegistration
            {
                username = "",
                email = "invalid-email",
                password = "", 
                captchToken = "invalidCaptchaToken"
            };

            var mockSender = new Mock<ISender>();
            var mockValidator = new Mock<IValidator<UserRegistration>>();

            mockValidator.Setup(v => v.ValidateAsync(userData, default))
                .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("username", "Username is required") }));

            var controller = new UserManagementController(mockSender.Object, mockValidator.Object);

            // Act
            var result = await controller.UserRegistration(userData);

            // Assert
            var actionResult = Assert.IsType<ActionResult<LoginResponse>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Username is required", badRequestResult.Value); // Check error message
        } 
        
        //User login test cases
        
        [Fact]
        public async Task UserLogin_ValidCredentials_ReturnsOkWithLoginResponse()
        {
            // Arrange
            var loginUser = new LoginUser
            {
                username = "testuser",
                password = "SecurePassword123",
                captchToken = "validCaptchaToken"
            };

            var mockSender = new Mock<ISender>();
            var expectedResponse = new LoginResponse { token = "validToken" };

            mockSender.Setup(s => s.Send(It.IsAny<LoginUserCommand>(), default))
                .ReturnsAsync(expectedResponse);

            var controller = new UserManagementController(mockSender.Object, null);

            // Act
            var result = await controller.UserLogin(loginUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualResponse = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal(expectedResponse.token, actualResponse.token);
        }
        
        [Fact]
        public async Task UserLogin_MissingCredentials_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var loginUser = new LoginUser
            {
                password = "SecurePassword123",
                captchToken = "validCaptchaToken"
            };

            var mockSender = new Mock<ISender>();
            var expectedResponse = new LoginResponse { token = "validToken" };

            mockSender.Setup(s => s.Send(It.IsAny<LoginUserCommand>(), default))
                .ReturnsAsync(expectedResponse);

            var controller = new UserManagementController(mockSender.Object, null);

            // Act
            var result = await controller.UserLogin(loginUser);

            // Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var actualResponse = Assert.IsType<string>(okResult.Value);
            Assert.Equal(Constants.ErrorMessages.UserLogin, actualResponse);
        }
        
        [Fact]
        public async Task UserLogin_InvalidCredentialsPassword_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var loginUser = new LoginUser
            {
                username = "wronguser",
                password = "password",
                captchToken = "validCaptchaToken"
            };

            var mockSender = new Mock<ISender>();
            mockSender.Setup(s => s.Send(It.IsAny<LoginUserCommand>(), default))
                .ThrowsAsync(new UnauthorizedAccessException(Constants.ErrorMessages.InvalidPassword));

            var controller = new UserManagementController(mockSender.Object, null);

            // Act
            Exception exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => controller.UserLogin(loginUser));

            // Assert
            Assert.Equal(Constants.ErrorMessages.InvalidPassword, exception.Message);
        }
    }
}