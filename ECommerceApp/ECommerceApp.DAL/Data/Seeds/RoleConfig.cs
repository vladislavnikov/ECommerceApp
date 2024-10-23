using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ECommerceApp.DAL.Data.Configuration
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public static Guid AdminRoleId { get; private set; }
        public static Guid UserRoleId { get; private set; }

        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            AdminRoleId = Guid.NewGuid(); 
            UserRoleId = Guid.NewGuid(); 

            builder.HasData(CreateRoles());
        }

        private IdentityRole<Guid>[] CreateRoles()
        {
            return new IdentityRole<Guid>[]
            {
                new IdentityRole<Guid> { Id = AdminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<Guid> { Id = UserRoleId, Name = "User", NormalizedName = "USER" }
            };
        }
    }
}
