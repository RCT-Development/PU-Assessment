using PU.Core.DTO.Request;
using PU.Core.DTO.Response;
using PU.Core.Models;

namespace PU.Core.Services.Contracts
{
    public interface IGroupService : IBaseService<Group>
    {
        Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup();
        Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId);
        Task AddGroupUserAsync(Guid groupId, Guid userId);
        Task RemoveGroupUserAsync(Guid groupId, Guid userId);
        Task<IEnumerable<Permission>> GetGroupPermissionsAsync(Guid groupId);
        Task AddGroupPermissionAsync(Guid groupId, Guid permissionId);
        Task RemoveGroupPermissionAsync(Guid groupId, Guid permissionId);

    }
}
