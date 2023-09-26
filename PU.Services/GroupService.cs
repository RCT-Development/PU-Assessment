using PU.Core.DTO.Request;
using PU.Core.DTO.Response;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task CreateGroup(Group model)
        {
            await _groupRepository.CreateAsync(model);
        }
        public async Task DeleteGroup(Guid id)
        {
            var group = await _groupRepository.GetAsync(id);
            if (group == null)
            {
                throw new NotFoundException($"Group: {id} does not exist");
            }
            await _groupRepository.DeleteAsync(group);
        }
        public async Task<Group> GetGroup(Guid id)
        {
            return await _groupRepository.GetAsync(id);
        }
        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup()
        {
            return await _groupRepository.GetUserCountPerGroup();
        }
        public async Task UpdateGroup(Group model)
        {
            await _groupRepository.UpdateAsync(model);
        }
        public async Task<IEnumerable<User>> GetGroupUsers(Guid groupId)
        {
            return await _groupRepository.GetGroupUsers(groupId);
        }
        public async Task AddGroupUser(Guid groupId, Guid userId)
        {
            await _groupRepository.AddGroupUser(groupId, userId);
        }
        public async Task RemoveGroupUser(Guid groupId, Guid userId)
        {
            await _groupRepository.RemoveGroupUser(groupId, userId);
        }
        public async Task<IEnumerable<Permission>> GetGroupPermissions(Guid groupId)
        {
            return await _groupRepository.GetGroupPermissions(groupId);
        }
        public async Task AddGroupPermission(Guid groupId, Guid permissionId)
        {
            await _groupRepository.AddGroupPermission(groupId, permissionId);
        }
        public async Task RemoveGroupPermission(Guid groupId, Guid permissionId)
        {
            await _groupRepository.RemoveGroupPermission(groupId,permissionId);
        }
    }
}
