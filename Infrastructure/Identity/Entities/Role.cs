using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entities;
public class Role : IdentityRole
{
    public ICollection<UserRole> UserRoles { get; set; }
}
