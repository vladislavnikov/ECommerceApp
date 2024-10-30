using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.Business.Model.Request;
using ECommerceApp.Business.Model.Response;
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