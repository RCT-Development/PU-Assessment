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
        public async Task<IEnumerable<Group>> Get()
        {
            return await _groupService.GetGroups();
        }

        [HttpGet("UserCount")]
        public async Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup()
        {
            return await _groupService.GetUserCountPerGroup();
        }

        [HttpGet("{groupId}")]
        public async Task<Group> Get(Guid groupId)
        {
            return await _groupService.GetGroup(groupId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupRequest model)
        {
            var group = _mapper.Map<Group>(model);
            await _groupService.CreateGroup(group);
            return Ok();
        }

        [HttpPut("{groupId}")]
        public async Task<IActionResult> Put(Guid groupId, [FromBody] GroupRequest model)
        {
            var group = _mapper.Map<Group>(model);
            group.Id = groupId;
            await _groupService.UpdateGroup(group);
            return Ok();
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> Delete(Guid groupId)
        {
            await _groupService.DeleteGroup(groupId);
            return Ok();
        }

        [HttpGet("{groupId}/Users")]
        public async Task<IEnumerable<User>> GetGroupUsers(Guid groupId)
        {
            return await _groupService.GetGroupUsers(groupId);
        }

        [HttpPost("{groupId}/Users/{userId}")]
        public async Task<IActionResult> AddGroupUser(Guid groupId, Guid userId)
        {
            await _groupService.AddGroupUser(groupId, userId);
            return Ok();
        }

        [HttpDelete("{groupId}/Users/{userId}")]
        public async Task<IActionResult> RemoveGroupUser(Guid groupId, Guid userId)
        {
            await _groupService.RemoveGroupUser(groupId,userId);
            return Ok();
        }

        [HttpGet("{groupId}/Permissions")]
        public async Task<IEnumerable<Permission>> GetGroupPermissions(Guid groupId)
        {
            return await _groupService.GetGroupPermissions(groupId);
        }

        [HttpPost("{groupId}/Permissions/{permissionId}")]
        public async Task<IActionResult> AddGroupPermission(Guid groupId, Guid permissionId)
        {
            await _groupService.AddGroupPermission(groupId, permissionId);
            return Ok();
        }

        [HttpDelete("{groupId}/Permissions/{permissionId}")]
        public async Task<IActionResult> RemoveGroupPermission(Guid groupId, Guid permissionId)
        {
            await _groupService.RemoveGroupPermission(groupId,permissionId);
            return Ok();
        }
    }
}
