using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.Model.Request;
using ECommerceApp.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Updates the user profile.
        /// </summary>
        /// <param name="userUpdateModel">The user update model containing user details to update.</param>
        /// <returns>Returns the updated user profile.</returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserUpdateRequest userUpdateModel)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = _mapper.Map<UserUpdateDto>(userUpdateModel);

            var updatedUser = await _userRepository.UpdateUser(userId, user);

            var responseModel = _mapper.Map<UserUpdateResponse>(updatedUser);

            return Ok(responseModel);
        }

        /// <summary>
        /// Updates the user's password.
        /// </summary>
        /// <param name="passwordUpdateModel">The password update model containing new password details.</param>
        /// <returns>No content if the update was successful.</returns>
        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateRequest passwordUpdateModel)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var passDto = _mapper.Map<PasswordUpdateDto>(passwordUpdateModel);

            var result = await _userRepository.UpdatePasswordAsync(userId, passDto);

            if (!result)
            {
                return Forbid();
            }

            return NoContent();
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <returns>Returns the user profile details.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user ID.");
            }

            var userProfile = await _userRepository.GetUserProfileAsync(userId);

            var responseModel = _mapper.Map<UserInfoResponse>(userProfile);

            return Ok(responseModel);
        }
    }
}