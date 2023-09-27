using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PU.Core.DTO.Request;
using PU.Core.DTO.Response;
using PU.Core.Models;
using PU.Core.Services.Contracts;

namespace PU.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService,
                               IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _groupService.GetAllAsync();
        }

        [HttpGet("UserCount")]
        public async Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup()
        {
            return await _groupService.GetUserCountPerGroup();
        }

        [HttpGet("{groupId}")]
        public async Task<Group> GetAsync(Guid groupId)
        {
            return await _groupService.GetAsync(groupId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GroupRequest model)
        {
            var group = _mapper.Map<Group>(model);
            await _groupService.CreateAsync(group);
            return Ok();
        }

        [HttpPut("{groupId}")]
        public async Task<IActionResult> UpdateAsync(Guid groupId, [FromBody] GroupRequest model)
        {
            var group = _mapper.Map<Group>(model);
            group.Id = groupId;
            await _groupService.UpdateAsync(group);
            return Ok();
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteAsync(Guid groupId)
        {
            await _groupService.DeleteAsync(groupId);
            return Ok();
        }

        [HttpGet("{groupId}/Users")]
        public async Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId)
        {
            return await _groupService.GetGroupUsersAsync(groupId);
        }

        [HttpPost("{groupId}/Users/{userId}")]
        public async Task<IActionResult> AddGroupUserAsync(Guid groupId, Guid userId)
        {
            await _groupService.AddGroupUserAsync(groupId, userId);
            return Ok();
        }

        [HttpDelete("{groupId}/Users/{userId}")]
        public async Task<IActionResult> RemoveGroupUserAsync(Guid groupId, Guid userId)
        {
            await _groupService.RemoveGroupUserAsync(groupId,userId);
            return Ok();
        }

        [HttpGet("{groupId}/Permissions")]
        public async Task<IEnumerable<Permission>> GetGroupPermissionsAsync(Guid groupId)
        {
            return await _groupService.GetGroupPermissionsAsync(groupId);
        }

        [HttpPost("{groupId}/Permissions/{permissionId}")]
        public async Task<IActionResult> AddGroupPermissionAsync(Guid groupId, Guid permissionId)
        {
            await _groupService.AddGroupPermissionAsync(groupId, permissionId);
            return Ok();
        }

        [HttpDelete("{groupId}/Permissions/{permissionId}")]
        public async Task<IActionResult> RemoveGroupPermissionAsync(Guid groupId, Guid permissionId)
        {
            await _groupService.RemoveGroupPermissionAsync(groupId,permissionId);
            return Ok();
        }
    }
}
