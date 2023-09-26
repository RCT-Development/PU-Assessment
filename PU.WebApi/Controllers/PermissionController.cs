using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PU.Core.DTO.Request;
using PU.Core.Models;
using PU.Core.Services.Contracts;

namespace PU.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionService permissionService,
                                    IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<Permission>> Get()
        {
            return await _permissionService.GetPermissions();
        }

        [HttpGet("{permissionId}")]
        public async Task<Permission> Get(Guid permissionId)
        {
            return await _permissionService.GetPermission(permissionId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionRequest model)
        {
            var permission = _mapper.Map<Permission>(model);
            await _permissionService.CreatePermission(permission);
            return Ok();
        }

        [HttpPut("{permissionId}")]
        public async Task<IActionResult> Put(Guid permissionId, [FromBody] PermissionRequest model)
        {
            var permission = _mapper.Map<Permission>(model);
            permission.Id = permissionId;
            await _permissionService.UpdatePermission(permission);
            return Ok();
        }

        [HttpDelete("{permissionId}")]
        public async Task<IActionResult> Delete(Guid permissionId)
        {
            await _permissionService.DeletePermission(permissionId);
            return Ok();
        }
    }
}
