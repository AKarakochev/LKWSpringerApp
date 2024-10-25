using Microsoft.AspNetCore.Identity;

namespace LKWSpringerApp.Web.Services
{
    public class IdentityDataSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityDataSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedRolesAndAdminUserAsync()
        {
            var roles = new[] { "Administrator", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string email = "admin@admin.com";
            string password = "Admin1!";

            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var adminUser = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(adminUser, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}
