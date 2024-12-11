namespace LKWSpringerApp.Web.ViewModels.Admin.UserManagement
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<RoleViewModel> Roles { get; set; } = new();
    }

    public class RoleViewModel
    {
        public string RoleName { get; set; } = null!;
        public bool IsSelected { get; set; }
    }
}
