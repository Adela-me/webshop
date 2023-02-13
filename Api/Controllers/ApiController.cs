using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private ISender mediator = null!;
    protected ISender Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);

        return result switch
        {
            IValidationResult validationResult => BadRequest(
                CreateProblemDetails("Validation Error", StatusCodes.Status400BadRequest, result.Error, validationResult.Errors)),
            _ => BadRequest(CreateProblemDetails("Bad Request", StatusCodes.Status400BadRequest, result.Error)),
        };

    }

    protected ActionResult HandleResult(Result result)
    {
        if (result == null)
            return NotFound();

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Error);
    }

    private static ProblemDetails CreateProblemDetails(
        string title, int status, Error error, Error[]? errors = null)
        => new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

}
