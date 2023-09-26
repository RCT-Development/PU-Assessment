using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PU.Core.Models;
using PU.MVCWebApp.Services.Contracts;
using System.Net.Http;

namespace PU.MVCWebApp.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public async Task<IActionResult> Index()
        {
            var permissions = await _permissionService.GetAll();
            return View(permissions);
        }
        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var permission = await _permissionService.GetById(id);
            return View("AddOrUpdate", permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(Permission permission)
        {
            HttpResponseMessage response;
            if (permission?.Id == Guid.Empty)
            {
                response = await _permissionService.Create(permission);
            }
            else
            {
                response = await _permissionService.Update(permission);
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
            await _permissionService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
