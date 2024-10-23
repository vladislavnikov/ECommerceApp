using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ECommerceApp.DAL.Data.Configuration
{
    public class UserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        private readonly Guid _adminUserId;
        private readonly Guid _adminRoleId;

        public UserRoleConfig(Guid adminUserId, Guid adminRoleId)
        {
            _adminUserId = adminUserId;
            _adminRoleId = adminRoleId;
        }

        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(CreateUserRoles());
        }

        private IdentityUserRole<Guid>[] CreateUserRoles()
        {
            return new[]
            {
                new IdentityUserRole<Guid>
                {
                    UserId = _adminUserId,
                    RoleId = _adminRoleId
                }
            };
        }
    }
}
