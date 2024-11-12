using AutoMapper;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.DTO.ProductRating;
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
                cfg.CreateMap<ProductRating, ProductRatingDto>().ReverseMap();  
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
        public async Task SearchGamesAsync_ShouldReturnPaginatedAndFilteredProducts()
        {
            var product1 = new Product
            {
                Name = "Action Game",
                Genre = "Action",
                Background = "background1.jpg",
                Logo = "logo1.png",
                DateCreated = DateTime.UtcNow
            };

            var product2 = new Product
            {
                Name = "Adventure Game",
                Genre = "Adventure",
                Background = "background2.jpg",
                Logo = "logo2.png",
                DateCreated = DateTime.UtcNow
            };

            var product3 = new Product
            {
                Name = "Action Adventure Game",
                Genre = "Action",
                Background = "background3.jpg",
                Logo = "logo3.png",
                DateCreated = DateTime.UtcNow
            };

            _context.Products.AddRange(product1, product2, product3);
            await _context.SaveChangesAsync();

            // Act 
            var term = "Action"; 
            var limit = 2;  
            var offset = 0; 
            var result = await _repository.SearchGamesAsync(term, limit, offset);

            // Assert 
            Assert.Equal(2, result.Count); 
            Assert.Contains(result, p => p.Name == "Action Game");
            Assert.Contains(result, p => p.Name == "Action Adventure Game");

            var resultWithOffset = await _repository.SearchGamesAsync(term, limit, 1);
            Assert.Equal(1, resultWithOffset.Count); 
            Assert.Contains(resultWithOffset, p => p.Name == "Action Adventure Game");
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

        [Fact]
        public async Task GetProduct_ShouldReturnProductDto()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product to Retrieve",
                Platform = Platforms.PC,
                Genre = "RPG",
                Price = 19.99m,
                Rating = Rating.A,
                DateCreated = DateTime.UtcNow,
                Background = "background.jpg",   
                Logo = "logo.jpg"                
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProduct(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Platform.ToString(), result.Platform);
            Assert.Equal(product.Price, result.Price);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnFilteredProductList()
        {
            // Arrange
            _context.Products.AddRange(new List<Product>
    {
        new Product { Name = "Action Game", Genre = "Action", Platform = Platforms.PC, Rating = Rating.T, Price = 20, Background = "background1.jpg", Logo = "logo1.jpg" },
        new Product { Name = "Adventure Game", Genre = "Adventure", Platform = Platforms.Xbox, Rating = Rating.E, Price = 30, Background = "background2.jpg", Logo = "logo2.jpg" },
        new Product { Name = "RPG Game", Genre = "RPG", Platform = Platforms.PC, Rating = Rating.A, Price = 50, Background = "background3.jpg", Logo = "logo3.jpg" }
    });
            await _context.SaveChangesAsync();

            var listDto = new ProductListDto
            {
                Genre = "Action",
                Age = "T",
                SortBy = "Price",
                Order = "asc",
                Page = 1,
                PageSize = 10
            };

            // Act
            var result = await _repository.GetProducts(listDto);

            // Assert
            Assert.NotNull(result.Products);
            Assert.Single(result.Products);
            Assert.Equal("Action Game", result.Products[0].Name);
        }


        [Fact]
        public async Task RateProduct_ShouldAddOrUpdateRating()
        {
            // Arrange
            var product = new Product
            {
                Name = "Rateable Game",
                Platform = Platforms.PC,
                Rating = Rating.T,
                DateCreated = DateTime.UtcNow,
                Price = 29.99m,
                Background = "background.jpg",   // Added required property
                Genre = "Adventure",             // Added required property
                Logo = "logo.jpg"                // Added required property
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var userId = Guid.NewGuid().ToString();
            var ratingDto = new ProductRatingDto
            {
                ProductId = product.Id,
                Rating = 4
            };

            // Act
            var result = await _repository.RateProduct(userId, ratingDto);

            // Assert
            var updatedRating = await _context.ProductRatings.FirstOrDefaultAsync(r => r.ProductId == product.Id && r.UserId == Guid.Parse(userId));
            Assert.NotNull(updatedRating);
            Assert.Equal(ratingDto.Rating, updatedRating.Rating);
        }


        [Fact]
        public async Task RemoveRatingAsync_ShouldDeleteUserRating()
        {
            // Arrange
            var product = new Product
            {
                Name = "Rated Game",
                Platform = Platforms.PC,
                Rating = Rating.E,
                DateCreated = DateTime.UtcNow,
                Price = 49.99m,
                Background = "background.jpg",
                Genre = "Puzzle",
                Logo = "logo.jpg"
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var userId = Guid.NewGuid().ToString();
            var productRating = new ProductRating
            {
                ProductId = product.Id,
                UserId = Guid.Parse(userId),
                Rating = 5
            };
            _context.ProductRatings.Add(productRating);
            await _context.SaveChangesAsync();

            // Act
            await _repository.RemoveRatingAsync(userId, product.Id);

            // Assert
            var deletedRating = await _context.ProductRatings
                .FirstOrDefaultAsync(r => r.ProductId == product.Id && r.UserId == Guid.Parse(userId));
            Assert.Null(deletedRating);
        }


    }
}
