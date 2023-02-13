using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Identity.Authentication;
public class HttpContextService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId;
    }

    public void SetTokenInCookie(string token)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Append("token", token,
             new CookieOptions
             {
                 Expires = DateTime.Now.AddDays(1),
                 HttpOnly = true,
                 Secure = true,
                 IsEssential = true,
                 SameSite = SameSiteMode.None
             });
    }
}
