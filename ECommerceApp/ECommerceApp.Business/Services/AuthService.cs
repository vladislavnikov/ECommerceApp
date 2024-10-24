using ECommerceApp.Business.Contract;
using ECommerceApp.Business.DTOs;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            JwtService jwtService,
            EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<(bool success, string token)> SignInAsync(SignInRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return (false, "Email not confirmed");
                }

                var token = _jwtService.GenerateJwtToken(user);
                return (true, token);
            }

            return (false, null);
        }

        public async Task<(bool success, string message)> SignUpAsync(SignUpRequestModel model, IUrlHelper urlHelper, HttpRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = urlHelper.Action("EmailConfirm", "Auth", new { userId = user.Id, token }, request.Scheme);

            return (true, "Confirm email");
        }

        public async Task<(bool success, string message)> ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
