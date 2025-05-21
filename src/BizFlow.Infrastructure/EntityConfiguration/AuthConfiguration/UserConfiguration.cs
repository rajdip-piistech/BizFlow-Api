using BizFlow.Domain.Model.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BizFlow.Infrastructure.EntityConfiguration.AuthConfiguration;

public class UserConfiguration: IEntityTypeConfiguration<IdentityModel.User>
{
    public void Configure(EntityTypeBuilder<IdentityModel.User> builder)
    {
        var hasher = new PasswordHasher<IdentityModel.User>();
        builder.HasData(new IdentityModel.User
        {
            Id = 1,
            Email = "admin@localhost.com",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            UserName = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "P@ssword1"),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        }, new IdentityModel.User
        {
            Id = 2,
            Email = "employee@localhost.com",
            NormalizedEmail = "EMPLOYEE@LOCALHOST.COM",
            UserName = "employee@localhost.com",
            NormalizedUserName = "EMPLOYEE@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "P@ssword1"),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        }, new IdentityModel.User
        {
            Id = 3,
            Email = "trainer@localhost.com",
            NormalizedEmail = "TRAINER@LOCALHOST.COM",
            UserName = "trainer@localhost.com",
            NormalizedUserName = "TRAINER@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "P@ssword1"),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        }, new IdentityModel.User
        {
            Id = 4,
            Email = "student@localhost.com",
            NormalizedEmail = "STUDENT@LOCALHOST.COM",
            UserName = "student@localhost.com",
            NormalizedUserName = "STUDENT@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "P@ssword1"),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        });
    }
}
