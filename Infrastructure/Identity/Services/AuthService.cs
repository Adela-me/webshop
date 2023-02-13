using Application.Common.Identity;
using Application.Common.Identity.DTOs;
using AutoMapper;
using Domain.Common;
using Infrastructure.Common.IdentityErrors;
using Infrastructure.Identity.Authentication;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Services;
public class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly JwtTokenGenerator jwtTokenGenerator;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly HttpContextService httpContextService;

    public AuthService(IMapper mapper, JwtTokenGenerator jwtTokenGenerator, UserManager<User> userManager,
            SignInManager<User> signInManager, HttpContextService httpContextService)
    {
        this.mapper = mapper;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.httpContextService = httpContextService;
    }

    public async Task<Result<string>> Register(RegisterUserDto registerData)
    {
        var existingUser = await userManager.FindByEmailAsync(registerData.Email);

        if (existingUser != null)
            return Result.Failure<string>(Errors.User.DuplicateEmail);

        var user = mapper.Map<User>(registerData);

        user.UserName = user.Email;

        var result = await userManager.CreateAsync(user, registerData.Password);

        if (!result.Succeeded)
            return Result.Failure<string>(new Error("User.NotSaved", result.Errors.First().Description));

        await userManager.AddToRoleAsync(user, "User");

        return user.Id;
    }

    public async Task<Result<string>> Login(LoginUserDto loginData)
    {
        var (email, password) = loginData;

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return Result.Failure<string>(Errors.Authentication.InvalidCredentials);

        var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

        if (!result.Succeeded)
            return Result.Failure<string>(Errors.Authentication.InvalidCredentials);

        var token = jwtTokenGenerator.GenerateToken(user);

        httpContextService.SetTokenInCookie(token);

        return Result.Success(user.Id);
    }

    public async Task<Result<CurrentUserDto>> GetCurrentUser()
    {
        var userId = httpContextService.GetUserId();

        if (userId is null)
            return Result.Failure<CurrentUserDto>(Errors.Authentication.NotAuthenticated);

        var user = await userManager.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));

        if (user is null)
            return Result.Failure<CurrentUserDto>(Errors.User.NotFound);

        return mapper.Map<CurrentUserDto>(user);
    }
}
