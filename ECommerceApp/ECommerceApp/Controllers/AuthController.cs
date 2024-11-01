using ECommerceApp.Business.Contract;
using ECommerceApp.Business.DTOs;
using ECommerceApp.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Signs in a user and returns a JWT token.
        /// </summary>
        /// <param name="model">The sign-in request model containing user information.</param>
        /// <returns>A JWT token if sign-in is successful; otherwise, an unauthorized response.</returns>
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The sign-up request model containing user information.</param>
        /// <returns>Status code 201 if successful, otherwise a bad request response.</returns>
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

        /// <summary>
        /// Confirms a user's email address.
        /// </summary>
        /// <param name="userId">The ID of the user to confirm.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>Ok response if email is confirmed; otherwise, a bad request response.</returns>
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
