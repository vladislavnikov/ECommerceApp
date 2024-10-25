using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Contract
{
    public interface IJwtService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}
