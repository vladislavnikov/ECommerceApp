using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.Model.Response;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlatformResponseModel>> GetTopPlatformsAsync()
        {
            return await _context.Products
              .GroupBy(p => p.Platform)
              .OrderByDescending(g => g.Count())
              .Take(3)
              .Select(g => new PlatformResponseModel
              {
                  PlatformName = g.Key.ToString(),  
                  ProductCount = g.Count()
              })
              .ToListAsync();
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
    }
}
