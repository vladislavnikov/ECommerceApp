using ECommerceApp.Business.Repository;
using ECommerceApp.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

public class UserRepositoryTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly ApplicationDbContext _context;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new ApplicationDbContext(options);

        var store = new Mock<IUserStore<ApplicationUser>>();
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
        var lookupNormalizer = new Mock<ILookupNormalizer>();
        var errorDescriber = new IdentityErrorDescriber();
        var serviceProvider = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<ApplicationUser>>>();

        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object,
            optionsAccessor.Object,
            passwordHasher,
            new List<IUserValidator<ApplicationUser>>(),
            new List<IPasswordValidator<ApplicationUser>>(),
            lookupNormalizer.Object,
            errorDescriber,
            serviceProvider.Object,
            logger.Object
        );

        _userRepository = new UserRepository(_context, _userManagerMock.Object);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userRepository.GetUserById(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.UserName, result.UserName);
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUser_WhenUserExists()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        user.UserName = "updateduser";

        // Act
        var updatedUser = await _userRepository.UpdateUser(user);

        // Assert
        Assert.Equal("updateduser", updatedUser.UserName);
    }

    [Fact]
    public async Task UpdateUser_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "nonexistent" };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userRepository.UpdateUser(user));
    }

    [Fact]
    public async Task UpdatePasswordAsync_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        var oldPassword = "oldPassword123!";
        var newPassword = "newPassword123!";

        _userManagerMock.Setup(um => um.CreateAsync(user, oldPassword)).ReturnsAsync(IdentityResult.Success);
        await _userManagerMock.Object.CreateAsync(user, oldPassword); 

        _userManagerMock.Setup(um => um.CheckPasswordAsync(user, oldPassword)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.ChangePasswordAsync(user, oldPassword, newPassword)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userRepository.UpdatePasswordAsync(user, oldPassword, newPassword);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdatePasswordAsync_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        var oldPassword = "oldPassword123!";
        var newPassword = "newPassword123!";
        var wrongPassword = "wrongPassword456!";

        await _userManagerMock.Object.CreateAsync(user, oldPassword);
        _userManagerMock.Setup(um => um.CheckPasswordAsync(user, oldPassword)).ReturnsAsync(false); // Simulate incorrect password

        // Act
        var result = await _userRepository.UpdatePasswordAsync(user, wrongPassword, newPassword);

        // Assert
        Assert.False(result);
    }

}
