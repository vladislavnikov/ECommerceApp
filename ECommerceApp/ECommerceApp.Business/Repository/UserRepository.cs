using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
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

    }
}
