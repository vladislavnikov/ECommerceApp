using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(Guid userId);
        Task<ApplicationUser> UpdateUser(ApplicationUser model);
    }
}
