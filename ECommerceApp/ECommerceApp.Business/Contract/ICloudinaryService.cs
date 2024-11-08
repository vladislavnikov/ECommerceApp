using Microsoft.AspNetCore.Http;

namespace ECommerceApp.Business.Contract
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
