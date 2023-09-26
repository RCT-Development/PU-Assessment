using PU.Core.DTO.Request;
using PU.Core.DTO.Response;
using PU.Core.Models;

namespace PU.Core.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup();
        Task<IEnumerable<User>> GetGroupUsers(Guid groupId);
        Task AddGroupUser(Guid groupId, Guid userId);
        Task RemoveGroupUser(Guid groupId, Guid userId);
        Task<IEnumerable<Permission>> GetGroupPermissions(Guid groupId);
        Task AddGroupPermission(Guid groupId, Guid permissionId);
        Task RemoveGroupPermission(Guid groupId, Guid permissionId);
    }
}
