using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entities;
public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhotoUrl { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }

}
