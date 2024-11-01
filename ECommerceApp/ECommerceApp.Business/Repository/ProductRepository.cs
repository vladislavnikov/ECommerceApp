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

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PlatformResponseModel>> GetTopPlatformsAsync()
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
                .Select(g => new PlatformResponseModel
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
    }
}
