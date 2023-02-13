using Application.Common.Identity;
using Application.Common.Interfaces.Services;
using Infrastructure.Identity;
using Infrastructure.Identity.Authentication;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDataContext>((sp, options) =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
        });

        services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<IdentityDataContext>();

        var passwordOptions = new PasswordOptions()
        {
            RequireDigit = false,
            RequireLowercase = false,
            RequiredLength = 4,
            RequireNonAlphanumeric = false,
            RequireUppercase = false,
        };

        services.Configure<IdentityOptions>(opt =>
        {
            opt.Password = passwordOptions;
        });

        return services;
    }

    public static IServiceCollection RegisterAuthentication(this IServiceCollection services,
            IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddCookie(options =>
            {
                options.Cookie.Name = "token";
            })
           .AddJwtBearer(o =>
           {
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidIssuer = jwtSettings.Issuer, // configuration["JwtSettings:Issuer"],
                   ValidAudience = jwtSettings.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
               };
               o.Events = new JwtBearerEvents
               {
                   OnMessageReceived = context =>
                   {
                       context.Token = context.Request.Cookies["token"];
                       return Task.CompletedTask;
                   }
               };
           });

        return services;
    }

    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>((sp, options) =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("WebShopConnectionString"));
        });

        //   services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddSingleton<JwtTokenGenerator>();
        services.AddSingleton<HttpContextService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAuthService, AuthService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
