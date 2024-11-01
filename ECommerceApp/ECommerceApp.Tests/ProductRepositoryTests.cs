using AutoMapper;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.Repository;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.DAL.Data.Models.Enum; // Include this for Platforms enum
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Business.Model.Model;

namespace ECommerceApp.Tests.Repository
{
    public class ProductRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _mapper = CreateMapper();
            _repository = new ProductRepository(_context, _mapper);
        }

        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();

                cfg.CreateMap<Product, ProductResponseModel>()
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform.ToString()));
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task SearchGamesAsync_ReturnsMatchingGames()
        {
            // Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Game A", Platform = Platforms.PC }, // Added Platform
                new Product { Name = "Game B", Platform = Platforms.Xbox },
                new Product { Name = "Another Game", Platform = Platforms.PlayStation },
                new Product { Name = "Game C", Platform = Platforms.NintendoSwitch }
            });
            await _context.SaveChangesAsync();

            string searchTerm = "Game";
            int limit = 2;
            int offset = 0;

            // Act
            var result = await _repository.SearchGamesAsync(searchTerm, limit, offset);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Game A");
            Assert.Contains(result, p => p.Name == "Game B");
        }

        [Fact]
        public async Task SearchGamesAsync_ReturnsEmptyList_WhenNoMatches()
        {
            // Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Different Game", Platform = Platforms.PC },
                new Product { Name = "Another Game", Platform = Platforms.Xbox }
            });
            await _context.SaveChangesAsync();

            string searchTerm = "Nonexistent Game";
            int limit = 2;
            int offset = 0;

            // Act
            var result = await _repository.SearchGamesAsync(searchTerm, limit, offset);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTopPlatformsAsync_ReturnsFewerThanThree_WhenNotEnoughPlatforms()
        {
            // Arrange
            _context.Products.RemoveRange(_context.Products);
            await _context.SaveChangesAsync();

            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Game A", Platform = Platforms.PC },
                new Product { Name = "Game B", Platform = Platforms.PlayStation }
            });
            await _context.SaveChangesAsync();

            var allProducts = await _context.Products.ToListAsync();

            // Act
            var result = await _repository.GetTopPlatformsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); 

            Assert.Equal("PlayStation", result[0].PlatformName);
            Assert.Equal(1, result[0].ProductCount);

            Assert.Equal("PC", result[1].PlatformName);
            Assert.Equal(1, result[1].ProductCount);
        }


    }
}
