using ECommerceApp.Business.Repository;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

public class UserRepositoryTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly ApplicationDbContext _context;
    private readonly UserRepository _userRepository;


}
