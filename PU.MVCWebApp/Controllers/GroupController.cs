using Microsoft.AspNetCore.Mvc;
using PU.Core.Models;
using PU.MVCWebApp.Models.GroupViewModels;
using PU.MVCWebApp.Services.Contracts;

namespace PU.MVCWebApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;

        public GroupController(IGroupService groupService,
                               IPermissionService permissionService,
                               IUserService userService)
        {
            _groupService = groupService;
            _permissionService = permissionService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetAll();
            return View(groups);
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var group = await _groupService.GetById(id);
            return View("AddOrUpdate", group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(Group group)
        {
            HttpResponseMessage response;
            if (group?.Id == Guid.Empty)
            {
                response = await _groupService.Create(group);
            }
            else
            {
                response = await _groupService.Update(group);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _groupService.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = new GroupDetailsViewModel
            {
                Group = await _groupService.GetById(id),
                Users = await _groupService.GetGroupUsers(id),
                Permissions = await _groupService.GetGroupPermissions(id)
            };

            return View(model);
        }

        public async Task<IActionResult> Users(Guid id)
        {
            var groupUsers = await _groupService.GetGroupUsers(id);
            var allUsers = await _userService.GetAll();

            var model = new GroupUserViewModel
            {
                GroupUsers = groupUsers,
                GroupId = id,
                AvailableUsers = allUsers.ExceptBy(groupUsers.Select(x => x.Id), x => x.Id).ToList()
            };
            

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(Guid userId, Guid groupId)
        {
            await _groupService.RemoveUserFromGroup(userId, groupId);
            return RedirectToAction("Users",new { id = groupId});
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Guid userId, Guid groupId)
        {
            await _groupService.AddUserToGroup(userId, groupId);
            return RedirectToAction("Users", new { id = groupId });
        }

        public async Task<IActionResult> Permissions(Guid id)
        {
            var groupPermissions = await _groupService.GetGroupPermissions(id);
            var allPermissions = await _permissionService.GetAll();

            var model = new GroupPermissionsViewModel
            {
                GroupPermissions = groupPermissions,
                GroupId = id,
                AvailablePermissions = allPermissions.ExceptBy(groupPermissions.Select(x => x.Id), x => x.Id).ToList()
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemovePermission(Guid permissionId, Guid groupId)
        {
            await _groupService.RemovePermissionFromGroup(permissionId, groupId);
            return RedirectToAction("Permissions", new { id = groupId });
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission(Guid permissionId, Guid groupId)
        {
            await _groupService.AddPermissionToGroup(permissionId, groupId);
            return RedirectToAction("Permissions", new { id = groupId });
        }        
    }
}
