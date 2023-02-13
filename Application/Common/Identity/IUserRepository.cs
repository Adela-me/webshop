using Application.Common.Identity.DTOs;
using Domain.Common;

namespace Application.Common.Identity;

public interface IUserRepository
{
    Task<Result<List<UserDto>>> GetAll();
}
