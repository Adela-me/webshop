using Application.Common.Identity.DTOs;
using Domain.Common;

namespace Application.Common.Identity;

public interface IAuthService
{
    Task<Result<string>> Register(RegisterUserDto registerData);

    Task<Result<string>> Login(LoginUserDto loginData);

    Task<Result<CurrentUserDto>> GetCurrentUser();
}
