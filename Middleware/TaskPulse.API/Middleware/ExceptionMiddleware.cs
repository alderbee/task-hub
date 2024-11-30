using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskPulse.Domain.Exceptions;
using TaskPulse.Domain.Helpers;

namespace TaskPulse.API.Middleware;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        
        var problemDetails = new ProblemDetails
        {
            Title = exception is GeneralException ? exception.Message : Constants.ErrorMessages.GeneralException
        };

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}