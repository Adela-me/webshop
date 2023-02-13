using Application.Common.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UsersController : ApiController
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        return HandleResult(await userRepository.GetAll());
    }
}
