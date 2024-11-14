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
    public DbSet<ProductRating> ProductRatings { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
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

        modelBuilder.Entity<ProductRating>()
                 .HasKey(pr => new { pr.ProductId, pr.UserId });

        modelBuilder.Entity<ProductRating>()
            .HasOne(pr => pr.Product)
            .WithMany(p => p.Ratings)
            .HasForeignKey(pr => pr.ProductId);

        modelBuilder.Entity<ProductRating>()
            .HasOne(pr => pr.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(pr => pr.UserId);

        modelBuilder.Entity<Order>()
           .HasOne(o => o.User)
           .WithMany(u => u.Orders)
           .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
    }
}
