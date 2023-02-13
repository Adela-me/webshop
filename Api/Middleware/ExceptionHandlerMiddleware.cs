using Domain.Common;
using System.Net;
using System.Text.Json;

namespace Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;

        try
        {
            await next(context);

            //if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //    return Result.Failure(Errors.User.Unauthenticated);

            //if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            //    return Result.Failure(Errors.User.Unauthorized);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            response.ContentType = "application/json";

            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (ex is WebShopException exception)
            {
                response.StatusCode = (int)exception.StatusCode;
            }

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                message = ex.Message
            });

            await response.WriteAsync(result);

        }
    }
}
