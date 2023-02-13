using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entities;
public class UserRole : IdentityUserRole<string>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
