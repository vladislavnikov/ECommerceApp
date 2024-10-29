using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> GetUserById(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<UserUpdateDto> UpdateUser(Guid userId, UserUpdateDto model)
        {
            var userToUpdate = await GetUserById(userId);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            userToUpdate.UserName = model.UserName;
            userToUpdate.Email = model.Email;
            userToUpdate.PhoneNumber = model.PhoneNumber;
            userToUpdate.AddressDelivery = model.AddressDelivery;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, PasswordUpdateDto model)
        {

            var user = await GetUserById(userId);
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.OldPassword);

            if (!passwordCheck)
            {
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            return result.Succeeded;
        }

        public async Task<UserUpdateDto> GetUserProfileAsync(Guid userId)
        {
            var userModel = await _userManager.FindByIdAsync(userId.ToString());

            var userDto = _mapper.Map<UserUpdateDto>(userModel);

            return userDto;
        }

    }
}