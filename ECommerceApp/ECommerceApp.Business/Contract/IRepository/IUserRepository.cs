using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(Guid userId);
        Task<ApplicationUser> UpdateUser(ApplicationUser model);
        Task<bool> UpdatePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
        Task<ApplicationUser> GetUserProfileAsync(Guid userId);
    }
}
