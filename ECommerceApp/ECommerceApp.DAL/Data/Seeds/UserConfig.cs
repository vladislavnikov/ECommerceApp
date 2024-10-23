using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ECommerceApp.DAL.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUser AdminUser { get; private set; }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Create the admin user
            AdminUser = CreateAdminUser();
            builder.HasData(AdminUser);
        }

        private ApplicationUser CreateAdminUser()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser()
            {
                Id = Guid.NewGuid(), 
                UserName = "AdminUser",
                NormalizedUserName = "ADMINUSER",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                EmailConfirmed = true 
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@1234");

            return adminUser;
        }
    }
}
