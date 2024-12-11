using LKWSpringerApp.Web.ViewModels.Admin.UserManagement;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<AllUsersViewModel>> GetAllUsersAsync();
        Task<bool> AssignUserToRoleAsync(string userId, string roleName);
        Task<bool> RemoveUserRoleAsync(string userId, string roleName);
        Task<bool> DeleteUserAsync(string userId);
    }
}
