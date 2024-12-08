using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Admin.UserManagement;
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

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
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

            return View(userViewModels);
        }
    }
}
