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
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetUsers();
        }

        [HttpGet("{userId}")]
        public async Task<User> Get(Guid userId)
        {
            return await _userService.GetUser(userId);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest model)
        {
            var user = _mapper.Map<User>(model);

            await _userService.CreateUser(user);
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserRequest model)
        {
            var user = _mapper.Map<User>(model);
            user.Id = userId;

            await _userService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }

        [HttpGet("Count")]
        public async Task<int> GetUserCount()
        {
            var users = await _userService.GetUsers();
            return users.ToList().Count;
        }
    }
}
