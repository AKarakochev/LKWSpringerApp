using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Web.ViewModels.Tour;
using LKWSpringerApp.Web.ViewModels.TourModels;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface ITourService
    {
        Task<PaginatedList<AllTourModel>> IndexGetAllOrderedByTourNameAsync(int pageIndex, int pageSize);

        Task<DetailsTourModel> GetTourDetailsByIdAsync(Guid id);

        Task<List<TourViewModel>> GetAllToursAsync();

        Task AddTourAsync(AddTourModel model);

        Task<bool> UpdateTourAsync(EditTourModel model);

        Task<bool> SoftDeleteTourAsync(Guid id);
    }
}
