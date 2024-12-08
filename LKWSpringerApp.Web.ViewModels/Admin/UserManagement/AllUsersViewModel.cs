namespace LKWSpringerApp.Web.ViewModels.Admin.UserManagement
{
    public class AllUsersViewModel
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}
