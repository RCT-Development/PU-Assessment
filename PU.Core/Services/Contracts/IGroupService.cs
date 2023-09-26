using PU.Core.DTO.Request;
using PU.Core.DTO.Response;
using PU.Core.Models;

namespace PU.Core.Services.Contracts
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroups();
        Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup();
        Task<Group> GetGroup(Guid groupId);
        Task CreateGroup(Group model);
        Task UpdateGroup(Group model);
        Task DeleteGroup(Guid groupId);
        Task<IEnumerable<User>> GetGroupUsers(Guid groupId);
        Task AddGroupUser(Guid groupId, Guid userId);
        Task RemoveGroupUser(Guid groupId, Guid userId);
        Task<IEnumerable<Permission>> GetGroupPermissions(Guid groupId);
        Task AddGroupPermission(Guid groupId, Guid permissionId);
        Task RemoveGroupPermission(Guid groupId, Guid permissionId);

    }
}
