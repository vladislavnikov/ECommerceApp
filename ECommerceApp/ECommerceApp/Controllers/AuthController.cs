using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Business.DTOs;
using ECommerceApp.Business.Services;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtService jwtService;

        public AuthController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager, JwtService _jwtService)
        {
            this.signInManager = _signInManager;
            this.userManager = _userManager;
            this.jwtService = _jwtService;
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel request)
        {
            var user = await this.userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized();
            } 

            var result = await this.signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var token = this.jwtService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await this.userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (await userManager.AddToRoleAsync(user, "User") == IdentityResult.Success)
                {
                    return Ok(new { Message = "User registered and assigned 'User' role successfully" });
                }
                else
                {
                    return BadRequest("Failed to assign 'User' role");
                }
            }

            return BadRequest(result.Errors);
        }

    }
}
