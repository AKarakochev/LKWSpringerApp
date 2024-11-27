using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Web.ViewModels.TourModels;
using LKWSpringerApp.Services.Data.Helpers;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IDriverService
    {
        Task<PaginatedList<AllDriverModel>> IndexGetAllOrderedBySecondNameAsync(int pageIndex, int pageSize);

        Task AddDriverAsync(AddDriverModel model);

        Task<DetailsDriverModel> GetDriverDetailsByIdAsync(Guid id);

        Task<List<DriverModel>> GetAllDriversAsync();

        Task<bool> UpdateDriverAsync(EditDriverModel model);

        Task<bool> SoftDeleteDriverAsync(Guid id);
        Task<bool> RemoveDriverFromTourAsync(Guid driverId, Guid tourId);
    }
}
