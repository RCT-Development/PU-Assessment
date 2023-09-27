using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PU.Core.Models;
using PU.MVCWebApp.Models;
using PU.MVCWebApp.Services.Contracts;

namespace PU.MVCWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new UserViewModel
            {
                Users = await _userService.GetAll(),
                UsersCount = await _userService.GetUserCount()
            };
            
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _userService.GetById(id);
            return View("AddOrUpdate", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(User user)
        {
            HttpResponseMessage response;
            if (user?.Id == Guid.Empty)
            {
                response = await _userService.Create(user);
            } else
            {
                response = await _userService.Update(user);
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
            await _userService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
