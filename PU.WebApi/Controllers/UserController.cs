using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PU.Core.DTO.Request;
using PU.Core.Models;
using PU.Core.Services.Contracts;

namespace PU.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
                              IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{userId}")]
        public async Task<User> GetAsync(Guid userId)
        {
            return await _userService.GetAsync(userId);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest model)
        {
            var user = _mapper.Map<User>(model);

            await _userService.CreateAsync(user);
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAsync(Guid userId, [FromBody] UserRequest model)
        {
            var user = _mapper.Map<User>(model);
            user.Id = userId;

            await _userService.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            await _userService.DeleteAsync(userId);
            return Ok();
        }

        [HttpGet("Count")]
        public async Task<int> GetUserCountAsync()
        {
            var users = await _userService.GetAllAsync();
            return users.ToList().Count;
        }
    }
}
