using PU.Core.DTO.Response;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class GroupService : ServiceBase<Group>, IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository) : base(groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup()
        {
            return await _groupRepository.GetUserCountPerGroup();
        }
        public async Task UpdateGroup(Group model)
        {
            await _groupRepository.UpdateAsync(model);
        }
        public async Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId)
        {
            return await _groupRepository.GetGroupUsers(groupId);
        }
        public async Task AddGroupUserAsync(Guid groupId, Guid userId)
        {
            await _groupRepository.AddGroupUser(groupId, userId);
        }
        public async Task RemoveGroupUserAsync(Guid groupId, Guid userId)
        {
            await _groupRepository.RemoveGroupUser(groupId, userId);
        }
        public async Task<IEnumerable<Permission>> GetGroupPermissionsAsync(Guid groupId)
        {
            return await _groupRepository.GetGroupPermissions(groupId);
        }
        public async Task AddGroupPermissionAsync(Guid groupId, Guid permissionId)
        {
            await _groupRepository.AddGroupPermission(groupId, permissionId);
        }
        public async Task RemoveGroupPermissionAsync(Guid groupId, Guid permissionId)
        {
            await _groupRepository.RemoveGroupPermission(groupId,permissionId);
        }
    }
}
