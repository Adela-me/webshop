using Application.Common.Identity;
using Application.Common.Identity.DTOs;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IMapper mapper;
    private readonly IdentityDataContext dataContext;

    public UserRepository(IMapper mapper, IdentityDataContext dataContext)
    {
        this.mapper = mapper;
        this.dataContext = dataContext;
    }
    public async Task<Result<List<UserDto>>> GetAll()
    {
        var users = await dataContext.Users.ToListAsync();

        return Result.Success(mapper.Map<List<UserDto>>(users));
    }
}
