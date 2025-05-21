using BizFlow.Domain.Model.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BizFlow.Infrastructure.EntityConfiguration.AuthConfiguration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityModel.UserRole>
{
    public void Configure(EntityTypeBuilder<IdentityModel.UserRole> builder)
    {
        builder.HasData(new IdentityModel.UserRole
        {
            RoleId = 1,
            UserId = 1,
        }, new IdentityModel.UserRole
        {
            RoleId = 2,
            UserId = 2,
        }, new IdentityModel.UserRole
        {
            RoleId = 3,
            UserId = 3,
        }, new IdentityModel.UserRole
        {
            RoleId = 4,
            UserId = 4,
        });
    }
}
