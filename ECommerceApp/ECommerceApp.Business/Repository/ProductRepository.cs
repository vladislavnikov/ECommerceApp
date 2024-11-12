using AutoMapper;
using Azure.Core;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Platform;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.DTO.ProductRating;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.DAL.Data.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto model)
        {
            var product = _mapper.Map<Product>(model);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var savedProduct = await _context.Products.FindAsync(product.Id);

            return _mapper.Map<ProductDto>(savedProduct);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            var dto  = _mapper.Map<ProductDto>(product);

            return dto;
        }

        public async Task<ProductListDto> GetProducts(ProductListDto list)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(list.Genre))
            {
                query = query.Where(p => p.Genre == list.Genre);
            }

            if (!string.IsNullOrEmpty(list.Age) && Enum.TryParse<Rating>(list.Age, out var age))
            {
                query = query.Where(p => p.Rating == age);
            }

            if (list.SortBy == "Rating")
            {
                query = list.Order == "asc" ? query.OrderBy(p => p.TotalRating) : query.OrderByDescending(p => p.TotalRating);
            }
            else if (list.SortBy == "Price")
            {
                query = list.Order == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
            }

            var totalItems = await query.CountAsync();
            var products = await query.Skip((list.Page - 1) * list.PageSize)
                                       .Take(list.PageSize)
                                       .Select(p => new ProductDto
                                       {
                                           Id = p.Id,
                                           Name = p.Name,
                                           Platform = p.Platform.ToString(),
                                           DateCreated = p.DateCreated,
                                           TotalRating = p.TotalRating,
                                           Price = p.Price,
                                           Genre = p.Genre,
                                           Rating = (int)p.Rating,
                                           Logo = p.Logo,
                                           Background = p.Background,
                                           Count = p.Count
                                       })
                                       .ToListAsync();

            list.Products = products;
            list.TotalItems = totalItems;

            return list;
        }

        public async Task<List<PlatfromDto>> GetTopPlatformsAsync()
        {
            var platformGroups = await _context.Products
                .Where(p => p.Platform != null) 
                .GroupBy(p => p.Platform)
                .Select(g => new
                {
                    PlatformName = g.Key.ToString(),
                    ProductCount = g.Count()
                })
                .ToListAsync();

            return platformGroups
                .OrderByDescending(g => g.ProductCount)
                .Take(3)
                .Select(g => new PlatfromDto
                {
                    PlatformName = g.PlatformName,
                    ProductCount = g.ProductCount
                })
                .ToList();
        }

        public async Task<ProductRatingDto> RateProduct(string userId, ProductRatingDto dto)
        {
            var productRating = await _context.ProductRatings
             .FirstOrDefaultAsync(pr => pr.ProductId == dto.ProductId && pr.UserId == Guid.Parse(userId));

            if (productRating != null)
            {
                productRating.Rating = dto.Rating;
            }
            else
            {
                productRating = new ProductRating
                {
                    ProductId = dto.ProductId,
                    UserId = Guid.Parse(userId),
                    Rating = dto.Rating
                };
                await _context.ProductRatings.AddAsync(productRating);
            }

            var reponseDto = _mapper.Map<ProductRatingDto>(productRating);

            await _context.SaveChangesAsync();
            return reponseDto;
        }

        public async Task RemoveRatingAsync(string userId, int productId)
        {
            var rating = await _context.ProductRatings
                .FirstOrDefaultAsync(r => r.ProductId == productId && r.UserId == Guid.Parse(userId));

            _context.ProductRatings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> SearchGamesAsync(string term, int limit, int offset)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(term))
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task UpdateProductAsync(ProductDto model)
        {
            var productToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            productToUpdate.Name = model.Name;
            productToUpdate.Platform = Enum.Parse<Platforms>(model.Platform);
            productToUpdate.TotalRating = model.TotalRating;
            productToUpdate.DateCreated = model.DateCreated;
            productToUpdate.Price = model.Price;
            productToUpdate.Rating = (Rating)model.Rating;
            productToUpdate.Logo = model.Logo;
            productToUpdate.Background = model.Background;
            productToUpdate.Count = model.Count;
            productToUpdate.Genre = model.Genre;

            await _context.SaveChangesAsync();
        }
    }
}
