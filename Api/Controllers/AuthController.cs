using Application.Common.Identity;
using Application.Common.Identity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController : ApiController
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDto loginData)
    {
        return HandleResult(await authService.Login(loginData));
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserDto registerData)
    {
        return HandleResult(await authService.Register(registerData));
    }

    [HttpGet("me")]
    public async Task<ActionResult> GetLoggedInUser()
    {
        return HandleResult(await authService.GetCurrentUser());
    }
}
