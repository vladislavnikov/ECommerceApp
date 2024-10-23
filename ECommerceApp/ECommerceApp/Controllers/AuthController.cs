using ECommerceApp.DAL.Data.Models;
using ECommerceApp.Business.DTOs;
using ECommerceApp.Business.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            JwtService jwtService,
            EmailService emailService,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest("Email not confirmed");
                }

                var token = _jwtService.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }



        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false
            };

            var role = await _roleManager.FindByNameAsync("User");

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(EmailConfirm), "Auth", new { userId = user.Id, token }, Request.Scheme);
            

            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpGet("emailConfirm")]
        public async Task<IActionResult> EmailConfirm(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return BadRequest("Invalid user ID");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }
    }
}
