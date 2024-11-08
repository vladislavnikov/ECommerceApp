using ECommerceApp.DAL.Data.Models.Enum;
using ECommerceApp.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.DAL.Data.Seeds
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(CreateExistingProducts());
        }

        private Product[] CreateExistingProducts()
        {
            return new Product[]
            {
                new Product
                {
                    Id = 1,
                    Name = "Cyberpunk 2077",
                    Platform = Platforms.PC,
                    DateCreated = new DateTime(2020, 12, 10),
                    TotalRating = 78,
                    Price = 59.99m,
                    Genre = "Action RPG",
                    Rating = Rating.A,
                    Logo = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/81a4e680815973.5cec6bcf6aa1a.jpg",
                    Background = "https://img.freepik.com/free-photo/cyberpunk-urban-scenery_23-2150712464.jpg",
                    Count = 150
                },
                new Product
                {
                    Id = 2,
                    Name = "The Witcher 3: Wild Hunt",
                    Platform = Platforms.PlayStation,
                    DateCreated = new DateTime(2015, 5, 19),
                    TotalRating = 92,
                    Price = 39.99m,
                    Genre = "RPG",
                    Rating = Rating.A,
                    Logo = "https://upload.wikimedia.org/wikipedia/fr/4/44/The_Witcher_3_Wild_Hunt_Logo.png",
                    Background = "https://c4.wallpaperflare.com/wallpaper/639/471/719/the-witcher-the-witcher-3-wild-hunt-geralt-of-rivia-sword-wallpaper-preview.jpg",
                    Count = 80
                },
                new Product
                {
                    Id = 3,
                    Name = "Minecraft",
                    Platform = Platforms.Xbox,
                    DateCreated = new DateTime(2011, 11, 18),
                    TotalRating = 95,
                    Price = 26.95m,
                    Genre = "Sandbox",
                    Rating = Rating.E,
                    Logo = "https://thumbs.dreamstime.com/b/minecraft-logo-online-game-dirt-block-illustrations-concept-design-isolated-186775550.jpg",
                    Background = "https://thumbs.dreamstime.com/b/twisted-woodland-scenery-resembling-minecraft-landscape-ai-generative-design-background-instagram-facebook-wall-painting-329315903.jpg",
                    Count = 500
                },
                new Product
                {
                    Id = 4,
                    Name = "Animal Crossing: New Horizons",
                    Platform = Platforms.NintendoSwitch,
                    DateCreated = new DateTime(2020, 3, 20),
                    TotalRating = 90,
                    Price = 59.99m,
                    Genre = "Simulation",
                    Rating = Rating.E,
                    Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdfEjpNy40itgijgBOLfOHvsRggrO9ViqoDQ&s",
                    Background = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbKRnhoxCDNKrFWG11mQvGnkAgk0OozaeQ8Q&s",
                    Count = 120
                },
                new Product
                {
                    Id = 5,
                    Name = "Genshin Impact",
                    Platform = Platforms.Mobile,
                    DateCreated = new DateTime(2020, 9, 28),
                    TotalRating = 85,
                    Price = 0.00m, 
                    Genre = "Action RPG",
                    Rating = Rating.T,
                    Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0gKGxPvb-I7k6wdByzZ5Y7VzBEFg_Dxc6NA&s",
                    Background = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdqRoYXkrm6WD1Sd4J6VxdEC9GXEA-Mf0Ngw&s",
                    Count = 1000
                }
            };
        }
    }
}
