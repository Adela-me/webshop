using Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Configurations;
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(r => new { r.UserId, r.RoleId });

        builder.HasData(
                new UserRole
                {
                    RoleId = "a394e587-d850-48b0-8222-32fcb416b818",
                    UserId = "1057adfe-ac90-41e9-a356-0e6a03212e7f"
                },
                new UserRole
                {
                    RoleId = "09cfc009-c49d-4a4c-bea6-9f9e94ff8d96",
                    UserId = "b10fb754-19f5-476e-a1b0-b52f23c72d85"
                });
    }
}
