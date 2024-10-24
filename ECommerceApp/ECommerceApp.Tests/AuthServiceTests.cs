using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using ECommerceApp.Business.Services;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.Business.DTOs;
using Microsoft.AspNetCore.Http;
using ECommerceApp.Business.Contract;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<RoleManager<IdentityRole<Guid>>> _roleManagerMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _userManagerMock = MockUserManager();
            _signInManagerMock = MockSignInManager();
            _roleManagerMock = MockRoleManager();
            _jwtServiceMock = new Mock<IJwtService>();
            _emailServiceMock = new Mock<IEmailService>();

            _authService = new AuthService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _roleManagerMock.Object,
                _jwtServiceMock.Object,
                _emailServiceMock.Object
            );
        }

        [Fact]
        public async Task SignInAsync_ReturnsSuccess_WhenValidCredentials()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "test@mail.com", Email = "test@mail.com", EmailConfirmed = true };
            var model = new SignInRequestModel { Email = "test@mail.com", Password = "password" };

            _userManagerMock.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, model.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            _jwtServiceMock.Setup(js => js.GenerateJwtToken(user)).Returns("jwt_token");

            // Act
            var result = await _authService.SignInAsync(model);

            // Assert
            Assert.True(result.success);
            Assert.Equal("jwt_token", result.token);
        }

        [Fact]
        public async Task SignInAsync_ReturnsFailure_WhenInvalidCredentials()
        {
            // Arrange
            var model = new SignInRequestModel { Email = "wrong@mail.com", Password = "wrongpassword" };

            _userManagerMock.Setup(um => um.FindByEmailAsync(model.Email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authService.SignInAsync(model);

            // Assert
            Assert.False(result.success);
            Assert.Null(result.token);
        }

        [Fact]
        public async Task SignUpAsync_ReturnsSuccess_WhenUserCreated()
        {
            // Arrange
            var model = new SignUpRequestModel { Email = "test@mail.com", Password = "password" };
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), model.Password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User")).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("email_confirmation_token");

            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(uh => uh.Action(It.IsAny<UrlActionContext>()))
                          .Returns("http://localhost/confirmation_link"); 

            // Act
            var result = await _authService.SignUpAsync(model, urlHelperMock.Object, new DefaultHttpContext().Request);

            // Assert
            Assert.True(result.success);
            Assert.Equal("Confirm email", result.message);
        }

        [Fact]
        public async Task ConfirmEmailAsync_ReturnsFailure_WhenConfirmationFails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var token = "invalid_token";
            var user = new ApplicationUser { Id = userId };

            _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.ConfirmEmailAsync(user, token)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

            // Act
            var result = await _authService.ConfirmEmailAsync(userId, token);

            // Assert
            Assert.False(result.success);
            Assert.Equal("Invalid token", result.message);
        }

        private Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<SignInManager<ApplicationUser>> MockSignInManager()
        {
            var userManager = MockUserManager();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            return new Mock<SignInManager<ApplicationUser>>(
                userManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
        }

        private Mock<RoleManager<IdentityRole<Guid>>> MockRoleManager()
        {
            var store = new Mock<IRoleStore<IdentityRole<Guid>>>();
            return new Mock<RoleManager<IdentityRole<Guid>>>(
                store.Object, null, null, null, null);
        }
    }
}
