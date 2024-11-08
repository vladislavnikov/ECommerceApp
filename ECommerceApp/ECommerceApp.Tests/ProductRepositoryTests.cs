using AutoMapper;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.Repository;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.DAL.Data.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerceApp.Tests.Repository
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _repository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>().ReverseMap();
            });

            _mapper = mapperConfig.CreateMapper();
            _repository = new ProductRepository(_context, _mapper);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldAddNewProduct()
        {
            // Arrange
            var request = new ProductDto
            {
                Name = "Test Game",
                Platform = "PC",
                Rating = (int)Rating.T,
                Price = 59.99m,
                Genre = "Action",
                Logo = "logo.jpg",
                Background = "background.jpg",
                Count = 10,
                DateCreated = DateTime.UtcNow
            };

            // Act
            var result = await _repository.CreateProductAsync(request);

            // Assert
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == "Test Game");
            Assert.NotNull(product);
            Assert.Equal(request.Name, product.Name);
            Assert.Equal(request.Platform, product.Platform.ToString());
            Assert.Equal(request.Price, product.Price);
            Assert.Equal(request.Genre, product.Genre);
            Assert.Equal(request.Logo, product.Logo);
            Assert.Equal(request.Background, product.Background);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldRemoveProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Game to Delete",
                Platform = Platforms.Xbox,
                DateCreated = DateTime.UtcNow,
                Genre = "Action",
                Logo = "logo.jpg",
                Background = "background.jpg"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteProductAsync(product.Id);

            // Assert
            var deletedProduct = await _context.Products.FindAsync(product.Id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task GetTopPlatformsAsync_ShouldReturnTopPlatforms()
        {
            // Arrange
            _context.Products.RemoveRange(_context.Products);
            await _context.SaveChangesAsync();

            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Game A", Platform = Platforms.PC, Genre = "Action", Logo = "logo_A.jpg", Background = "background_A.jpg" },
                new Product { Name = "Game B", Platform = Platforms.PlayStation, Genre = "Adventure", Logo = "logo_B.jpg", Background = "background_B.jpg" }
            });
            await _context.SaveChangesAsync();

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

        [Fact]
        public async Task SearchGamesAsync_ShouldReturnMatchingProducts()
        {
            // Arrange
            _context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Game A", Platform = Platforms.PC, Genre = "Action", Logo = "logo.jpg", Background = "background.jpg", DateCreated = DateTime.UtcNow },
                new Product { Name = "Game B", Platform = Platforms.Xbox, Genre = "Action", Logo = "logo.jpg", Background = "background.jpg", DateCreated = DateTime.UtcNow },
                new Product { Name = "Another Game", Platform = Platforms.PlayStation, Genre = "Action", Logo = "logo.jpg", Background = "background.jpg", DateCreated = DateTime.UtcNow }
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
        public async Task UpdateProductAsync_ShouldModifyExistingProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Old Game",
                Platform = Platforms.PC,
                Price = 29.99m,
                Rating = Rating.K,
                Logo = "old_logo.jpg",
                Background = "old_background.jpg",
                Count = 1,
                DateCreated = DateTime.UtcNow,
                Genre = "Action"
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var updatedModel = new ProductDto
            {
                Id = product.Id,
                Name = "Updated Game",
                Platform = "Xbox",
                Price = 39.99m,
                Rating = (int)Rating.T,
                Logo = "updated_logo.jpg",
                Background = "updated_background.jpg",
                Count = 2,
                Genre = "Adventure",
                DateCreated = product.DateCreated
            };

            // Act
            await _repository.UpdateProductAsync(updatedModel);

            // Assert
            var updatedProduct = await _context.Products.FindAsync(product.Id);
            Assert.Equal(updatedModel.Name, updatedProduct.Name);
            Assert.Equal(updatedModel.Platform, updatedProduct.Platform.ToString());
            Assert.Equal(updatedModel.Price, updatedProduct.Price);
            Assert.Equal((Rating)updatedModel.Rating, updatedProduct.Rating);
            Assert.Equal(updatedModel.Logo, updatedProduct.Logo);
            Assert.Equal(updatedModel.Background, updatedProduct.Background);
            Assert.Equal(updatedModel.Count, updatedProduct.Count);
            Assert.Equal(updatedModel.Genre, updatedProduct.Genre);
        }
    }
}
