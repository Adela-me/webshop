using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

        var hashier = new PasswordHasher<User>();

        builder.HasData(
                new User
                {
                    Id = "1057adfe-ac90-41e9-a356-0e6a03212e7f",
                    UserName = "jane-webshop@yopmail.com",
                    NormalizedUserName = "JANE-WEBSHOP@YOPMAIL.COM",
                    PasswordHash = hashier.HashPassword(null, "1234"),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane-webshop@yopmail.com",
                    NormalizedEmail = "JANE-WEBSHOP@YOPMAIL.COM",
                },
                 new User
                 {
                     Id = "b10fb754-19f5-476e-a1b0-b52f23c72d85",
                     UserName = "john-webshop@yopmail.com",
                     NormalizedUserName = "JOHN-WEBSHOP@YOPMAIL.COM",
                     PasswordHash = hashier.HashPassword(null, "1234"),
                     FirstName = "John",
                     LastName = "Doe",
                     Email = "john-webshop@yopmail.com",
                     NormalizedEmail = "JOHN-WEBSHOP@YOPMAIL.COM",
                 });
    }
}
