using ECommerceApp.DAL.Data.Configuration;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.DAL.Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Product> Products { get; set; }
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
        modelBuilder.ApplyConfiguration(new ProductConfig());

        var userRoleConfig = new UserRoleConfig(userConfig.AdminUser.Id, RoleConfig.AdminRoleId);
        modelBuilder.ApplyConfiguration(userRoleConfig);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Platform);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.DateCreated);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.TotalRating);
    }
}
