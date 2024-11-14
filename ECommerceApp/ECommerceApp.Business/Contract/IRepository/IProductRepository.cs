using ECommerceApp.Business.DTO.Platform;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.DTO.ProductRating;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IProductRepository
    {
        Task<List<PlatfromDto>> GetTopPlatformsAsync();
        Task<List<ProductDto>> SearchGamesAsync(string term, int limit, int offset);
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> CreateProductAsync(ProductDto model);
        Task UpdateProductAsync(ProductDto model);
        Task DeleteProductAsync(int productId);
        Task<ProductRatingDto> RateProduct(string userId, ProductRatingDto dto);
        Task RemoveRatingAsync(string userId, int productId);
        Task<ProductListDto> GetProducts(ProductListDto list);
    }
}
