using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Services.Data
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<AllUsersViewModel>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<AllUsersViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new AllUsersViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return userViewModels;
        }

        public async Task<IActionResult> EditRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var allRoles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new UserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = allRoles.Select(r => new RoleViewModel
                {
                    RoleName = r.Name,
                    IsSelected = userRoles.Contains(r.Name)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();

            // Add new roles
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add roles.");
                return View(model);
            }

            // Remove unselected roles
            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove roles.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
