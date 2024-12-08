using LKWSpringerApp.Web.ViewModels.Admin.UserManagement;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<AllUsersViewModel>> GetAllUsersAsync();
    }
}
