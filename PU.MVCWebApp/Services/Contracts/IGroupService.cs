using PU.Core.Models;

namespace PU.MVCWebApp.Services.Contracts
{
    public interface IGroupService : IBaseService<Group>
    {
        Task AddPermissionToGroup(Guid permissionId, Guid groupId);
        Task RemovePermissionFromGroup(Guid permissionId, Guid groupId);
        Task AddUserToGroup(Guid userId, Guid groupId);
        Task RemoveUserFromGroup(Guid userId, Guid groupId);
        Task<List<Permission>> GetGroupPermissions(Guid id);
        Task<List<User>> GetGroupUsers(Guid id);
    }
}
