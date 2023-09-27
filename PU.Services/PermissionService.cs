using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class PermissionService : ServiceBase<Permission>, IPermissionService
    {
        public PermissionService(IPermissionRepository permissionRepository) : base(permissionRepository)
        {
        }
    }
}
