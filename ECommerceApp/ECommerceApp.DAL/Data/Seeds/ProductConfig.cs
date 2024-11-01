using ECommerceApp.DAL.Data.Models.Enum;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.DAL.Data.Seeds
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(CreateProducts());
        }

        private Product[] CreateProducts()
        {
            return new Product[]
            {
               new Product
                {
                    Id = 1,
                    Name = "Game A",
                    Platform = Platforms.PC,
                    DateCreated = DateTime.Now.AddMonths(-5),
                    TotalRating = 85,
                    Price = 29.99m
                },
                new Product
                {
                    Id = 2,
                    Name = "Game B",
                    Platform = Platforms.PlayStation,
                    DateCreated = DateTime.Now.AddMonths(-3),
                    TotalRating = 90,
                    Price = 49.99m
                },
                new Product
                {
                    Id = 3,
                    Name = "Game C",
                    Platform = Platforms.Xbox,
                    DateCreated = DateTime.Now.AddMonths(-2),
                    TotalRating = 75,
                    Price = 39.99m
                },
                new Product
                {
                    Id = 4,
                    Name = "Game D",
                    Platform = Platforms.NintendoSwitch,
                    DateCreated = DateTime.Now.AddMonths(-1),
                    TotalRating = 88,
                    Price = 59.99m
                },
                new Product
                {
                    Id = 5,
                    Name = "Game E",
                    Platform = Platforms.Mobile,
                    DateCreated = DateTime.Now.AddDays(-10),
                    TotalRating = 95,
                    Price = 0.99m
                }
            };
        }
    }
}
