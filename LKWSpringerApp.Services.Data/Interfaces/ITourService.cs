using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Web.ViewModels.Tour;
using LKWSpringerApp.Web.ViewModels.TourModels;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface ITourService
    {
        Task<ICollection<AllTourModel>> IndexGetAllOrderedBySecondNameAsync();

        Task<DetailsTourModel> GetTourDetailsByIdAsync(Guid id);

        Task<List<TourViewModel>> GetAllToursAsync();

        Task AddTourAsync(AddTourModel model);

        Task<bool> UpdateTourAsync(EditTourModel model);

        Task<bool> SoftDeleteTourAsync(Guid id);
    }
}
