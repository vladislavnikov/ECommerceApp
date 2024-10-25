using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.DAL.Data.Models;
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

        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<ApplicationUser>(userUpdateDto);
            await _userRepository.UpdateUser(user);
            return Ok(userUpdateDto);
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateDto passwordUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userRepository.GetUserById(userId); 

            var result = await _userRepository.UpdatePasswordAsync(user, passwordUpdateDto.OldPassword, passwordUpdateDto.NewPassword);

            if (!result)
            {
                return Forbid();
            }

            return NoContent();
        }

    }
}
