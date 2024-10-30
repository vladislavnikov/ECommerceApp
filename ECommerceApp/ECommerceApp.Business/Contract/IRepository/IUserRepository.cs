using ECommerceApp.Business.DTO.User;
using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(Guid userId);
        Task<UserUpdateDto> UpdateUser(Guid userId, UserUpdateDto model);

        Task<bool> UpdatePasswordAsync(Guid userId, PasswordUpdateDto model);
        Task<UserUpdateDto> GetUserProfileAsync(Guid userId);
    }
}