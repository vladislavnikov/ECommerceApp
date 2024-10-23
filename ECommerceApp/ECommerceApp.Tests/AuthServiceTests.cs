using ECommerceApp.Business.DTOs;
using ECommerceApp.Business.Services;
using ECommerceApp.Controllers;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System;
using System.Threading.Tasks;

namespace ECommerceApp.Tests.Controllers
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly Mock<JwtService> _mockJwtService;
        private readonly AuthController _authController;

        public AuthServiceTests()
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var mockHttpContextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var mockUserClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object,
                mockHttpContextAccessor.Object,
                mockUserClaimsPrincipalFactory.Object,
                null, null, null, null);

            _mockJwtService = new Mock<JwtService>(null);

            _authController = new AuthController(
                _mockSignInManager.Object,
                _mockUserManager.Object,
                _mockJwtService.Object,
                null,
                null);
        }

        [Fact]
        public async Task SignIn_InvalidPassword_ReturnsUnauthorized()
        {
            // Arrange
            var signInRequest = new SignInRequestModel { Email = "user@mail.com", Password = "WrongPassword" };
            var user = new ApplicationUser { Email = signInRequest.Email, EmailConfirmed = true };
            _mockUserManager.Setup(um => um.FindByEmailAsync(signInRequest.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, signInRequest.Password)).ReturnsAsync(false);

            // Act
            var result = await _authController.SignIn(signInRequest);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task SignIn_EmailNotConfirmed_ReturnsBadRequest()
        {
            // Arrange
            var signInRequest = new SignInRequestModel { Email = "user@mail.com", Password = "Password123" };
            var user = new ApplicationUser { Email = signInRequest.Email, EmailConfirmed = false };
            _mockUserManager.Setup(um => um.FindByEmailAsync(signInRequest.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, signInRequest.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(false);

            // Act
            var result = await _authController.SignIn(signInRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email not confirmed", badRequestResult.Value);
        }

        [Fact]
        public async Task EmailConfirm_ValidUserAndToken_ReturnsNoContent()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId, Email = "user@mail.com" };
            _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.ConfirmEmailAsync(user, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authController.EmailConfirm(userId, "valid-token");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task EmailConfirm_InvalidUserId_ReturnsBadRequest()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
            _mockUserManager.Setup(um => um.FindByIdAsync(invalidUserId.ToString())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authController.EmailConfirm(invalidUserId, "some-token");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid user ID", badRequestResult.Value);
        }
    }
}
