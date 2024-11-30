# Task Hub

**Task Hub** is a full-stack web application designed to help users efficiently manage personal tasks. It provides features such as secure authentication, task management (CRUD), and follows the **CQRS** pattern with **MediatR** and **Clean Architecture** principles for better separation of concerns and maintainability.

## Features
- **User Authentication**: Secure login and registration with OAuth 2.0 or ASP.NET Identity, and enhanced security with Google Captcha.
- **Task Management**: Create, edit, delete, and organize tasks with properties like priority, deadline, and status.

## Technologies Used
- **Backend**: ASP.NET Core 8.0 (C#), Entity Framework Core
- **Frontend**: Angular (or React), Bootstrap
- **Database**: MSSQL with EF Core ORM
- **Authentication**: IdentityServer or OAuth 2.0 with Google Captcha
- **CQRS**: Implemented with MediatR for efficient handling of commands and queries.
- **Architecture**: Follows **Clean Architecture** principles for maintainable, testable, and scalable code.
- **Testing**: xUnit, Moq, Selenium (or Cypress)

## Testing & Quality Assurance
- **Unit Testing**: Core logic covered using xUnit and Moq.
- **Integration Testing**: API endpoint testing via Postman/Swagger.
- **UI Testing**: End-to-end testing with Selenium or Cypress.

