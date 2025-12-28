using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using ProjectDroit.Server.Dto;

namespace ProjectDroit.Server;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        HttpStatusCode statusCode;

        
        statusCode = HttpStatusCode.InternalServerError;

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var response = new ApiResponseError
        {
            Error = exception.Message
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        await httpContext.Response.WriteAsync(jsonResponse, cancellationToken: cancellationToken);

        return true;
    }

}