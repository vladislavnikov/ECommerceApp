using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Platform;
using ECommerceApp.Business.DTO.Product;
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
