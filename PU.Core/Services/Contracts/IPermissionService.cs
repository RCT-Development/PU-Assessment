using PU.Core.DTO.Request;
using PU.Core.Models;

namespace PU.Core.Services.Contracts
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission> GetPermission(Guid id);
        Task CreatePermission(Permission model);
        Task UpdatePermission(Permission model);
        Task DeletePermission(Guid id);
    }
}
