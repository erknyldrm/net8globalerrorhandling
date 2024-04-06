using Microsoft.AspNetCore.Diagnostics;

public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        string errorMessage = $"Bir hata oluştu:{exception.Message}";
        logger.LogError(errorMessage);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new 
        {   
            Title = "Server Error",
            Status = httpContext.Response.StatusCode,
            Message = errorMessage
        });

        return true;
    }
}