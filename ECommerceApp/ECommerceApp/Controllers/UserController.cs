using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<ApplicationUser>(userUpdateDto);
            await _userRepository.UpdateUser(user);
            return Ok(userUpdateDto);
        }

    }
}
