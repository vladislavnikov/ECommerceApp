using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerceApp.Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager  = userManager;
        }

        public async Task<ApplicationUser> GetUserById(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ApplicationUser> UpdateUser(ApplicationUser model)
        {
            var userToUpdate = await GetUserById(model.Id);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException($"User with ID {model.Id} not found.");
            }

            userToUpdate.UserName = model.UserName;
            userToUpdate.Email = model.Email;
            userToUpdate.PhoneNumber = model.PhoneNumber;
            userToUpdate.AddressDelivery = model.AddressDelivery;

            await _context.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<bool> UpdatePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            if (user == null)
            {
                return false;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, oldPassword);

            if (!passwordCheck)
            {
                return false; 
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> GetUserProfileAsync(Guid userId)
        {
           
            return await _userManager.FindByIdAsync(userId.ToString());
        }

    }
}
