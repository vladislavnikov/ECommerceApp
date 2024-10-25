using ECommerceApp.DAL.Data.Configuration;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var userConfig = new UserConfig();
        modelBuilder.ApplyConfiguration(userConfig);
        modelBuilder.ApplyConfiguration(new RoleConfig());

        var userRoleConfig = new UserRoleConfig(userConfig.AdminUser.Id, RoleConfig.AdminRoleId);
        modelBuilder.ApplyConfiguration(userRoleConfig);
    }
}
