using ECommerceApp.Business.DTO.User;
using ECommerceApp.Business.Repository;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class UserRepositoryTests
{
    private readonly UserRepository _userRepository;
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly ApplicationDbContext _dbContext;

    public UserRepositoryTests()
    {
        // Create in-memory database options
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

        // Initialize the in-memory database context
        _dbContext = new ApplicationDbContext(options);
        _userManagerMock = CreateUserManagerMock();

        // Initialize UserRepository with mocks
        _userRepository = new UserRepository(_dbContext, _userManagerMock.Object, null);
    }

    private Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        return userManagerMock;
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new ApplicationUser { Id = userId, UserName = "testuser", Email = "test@example.com" };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _userRepository.GetUserById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("testuser", result.UserName);
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new ApplicationUser { Id = userId, UserName = "olduser", Email = "old@example.com" };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var updateDto = new UserUpdateDto
        {
            UserName = "newuser",
            Email = "new@example.com",
            PhoneNumber = "123456789",
            AddressDelivery = "New Address"
        };

        // Act
        var result = await _userRepository.UpdateUser(userId, updateDto);

        // Assert
        var updatedUser = await _userRepository.GetUserById(userId);
        Assert.Equal("newuser", updatedUser.UserName);
        Assert.Equal("new@example.com", updatedUser.Email);
    }

    [Fact]
    public async Task UpdateUser_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updateDto = new UserUpdateDto { UserName = "newuser" };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _userRepository.UpdateUser(userId, updateDto));
    }

    [Fact]
    public async Task UpdatePasswordAsync_ShouldReturnTrue_WhenPasswordIsUpdated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new ApplicationUser { Id = userId, UserName = "testuser", Email = "test@example.com", PasswordHash = "oldpasswordhash" };

        // Setup UserManager mock to return user
        _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.CheckPasswordAsync(user, "oldpassword")).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.ChangePasswordAsync(user, "oldpassword", "newpassword")).ReturnsAsync(IdentityResult.Success);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var passwordUpdateDto = new PasswordUpdateDto { OldPassword = "oldpassword", NewPassword = "newpassword" };

        // Act
        var result = await _userRepository.UpdatePasswordAsync(userId, passwordUpdateDto);

        // Assert
        Assert.True(result);
    }
}
