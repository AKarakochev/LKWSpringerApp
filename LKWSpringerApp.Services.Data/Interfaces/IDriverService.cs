using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Web.ViewModels.TourModels;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IDriverService
    {
        Task<ICollection<AllDriverModel>> IndexGetAllOrderedBySecondNameAsync();

        Task AddDriverAsync(AddDriverModel model);

        Task<DetailsDriverModel> GetDriverDetailsByIdAsync(Guid id);

        Task<List<DriverModel>> GetAllDriversAsync();

        Task<bool> UpdateDriverAsync(EditDriverModel model);

        Task<bool> SoftDeleteDriverAsync(Guid id);
        Task<bool> RemoveDriverFromTourAsync(Guid driverId, Guid tourId);
    }
}
