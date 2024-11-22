using LKWSpringerApp.Web.ViewModels.Driver;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IDriverService
    {
        Task<ICollection<AllDriverModel>> IndexGetAllOrderedBySecondNameAsync();

        Task AddDriverAsync(AddDriverModel model);

        Task<DetailsDriverModel> GetDriverDetailsByIdAsync(Guid id);

        Task<bool> UpdateDriverAsync(EditDriverModel model);

        Task<bool> SoftDeleteDriverAsync(Guid id);
    }
}
