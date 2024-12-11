using LKWSpringerApp.Services.Data.Interfaces;
using static LKWSpringerApp.Common.SuccessMessagesConstants.UserManagement;
using static LKWSpringerApp.Common.ErrorMessagesConstants.UserManagement;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewData["AvailableRoles"] = roles;

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            bool assignResult = await _userService.AssignUserToRoleAsync(userId, role);
            if (await _userService.AssignUserToRoleAsync(userId, role))
            {
                TempData["SuccessMessage"] = RoleAssignSuccess;
            }
            else
            {
                TempData["ErrorMessage"] = RoleAssignFail;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            bool removeResult = await _userService.RemoveUserRoleAsync(userId, role);
            if (!removeResult)
            {
                TempData["ErrorMessage"] = RoleRemoveFail;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            bool deleteResult = await _userService.DeleteUserAsync(userId);
            if (!deleteResult)
            {
                TempData["ErrorMessage"] = UserDeleteFail;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
