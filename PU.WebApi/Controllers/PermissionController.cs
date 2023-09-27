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
        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _permissionService.GetAllAsync();
        }

        [HttpGet("{permissionId}")]
        public async Task<Permission> GetAsync(Guid permissionId)
        {
            return await _permissionService.GetAsync(permissionId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PermissionRequest model)
        {
            var permission = _mapper.Map<Permission>(model);
            await _permissionService.CreateAsync(permission);
            return Ok();
        }

        [HttpPut("{permissionId}")]
        public async Task<IActionResult> UpdateAsync(Guid permissionId, [FromBody] PermissionRequest model)
        {
            var permission = _mapper.Map<Permission>(model);
            permission.Id = permissionId;
            await _permissionService.UpdateAsync(permission);
            return Ok();
        }

        [HttpDelete("{permissionId}")]
        public async Task<IActionResult> DeleteAsync(Guid permissionId)
        {
            await _permissionService.DeleteAsync(permissionId);
            return Ok();
        }
    }
}
