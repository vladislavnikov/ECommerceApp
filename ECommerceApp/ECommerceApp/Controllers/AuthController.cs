using ECommerceApp.Business.Contract;
using ECommerceApp.Business.DTOs;
using ECommerceApp.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel model)
        {
            var (success, token) = await _authService.SignInAsync(model);

            if (success)
            {
                return Ok(new { Token = token });
            }

            return Unauthorized(token);
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel model)
        {
            var (success, message) = await _authService.SignUpAsync(model, Url, Request);

            if (success)
            {
                return StatusCode(201, message);
            }

            return BadRequest(message);
        }

        [AllowAnonymous]
        [HttpGet("emailConfirm")]
        public async Task<IActionResult> EmailConfirm(Guid userId, string token)
        {
            var (success, message) = await _authService.ConfirmEmailAsync(userId, token);

            if (success)
            {
                return Ok(message);
            }

            return BadRequest(message);
        }
    }
}
