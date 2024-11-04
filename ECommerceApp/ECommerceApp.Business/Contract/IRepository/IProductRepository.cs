using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.Model.Model;
using ECommerceApp.Business.Model.Request;
using ECommerceApp.Business.Model.Response;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IProductRepository
    {
        Task<List<PlatformResponseModel>> GetTopPlatformsAsync();
        Task<List<ProductDto>> SearchGamesAsync(string term, int limit, int offset);
        Task<ProductDto> GetProduct(int id);
        Task CreateProductAsync(ProductRequestModel model);
        Task UpdateProductAsync(ProductRequestModel model);

        Task DeleteProductAsync(int productId);
    }
}
