using PU.Core.DTO.Request;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task CreatePermission(Permission model)
        {
            await _permissionRepository.CreateAsync(model);
        }

        public async Task DeletePermission(Guid id)
        {
            var permission = await _permissionRepository.GetAsync(id);
            if (permission == null)
            {
                throw new NotFoundException($"Permission: {id} does not exist");
            }
            await _permissionRepository.DeleteAsync(permission);
        }

        public async Task<Permission> GetPermission(Guid id)
        {
            return await _permissionRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            return await _permissionRepository.GetAllAsync();
        }

        public async Task UpdatePermission(Permission model)
        {
            await _permissionRepository.UpdateAsync(model);
        }
    }
}
