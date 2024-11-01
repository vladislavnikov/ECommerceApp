using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.Model.Response;
using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IProductRepository
    {
        Task<List<PlatformResponseModel>> GetTopPlatformsAsync();
        Task<List<ProductDto>> SearchGamesAsync(string term, int limit, int offset);
    }
}
