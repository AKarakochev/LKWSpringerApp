using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Web.ViewModels.Tour;
using LKWSpringerApp.Web.ViewModels.TourModels;

using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Services.Data
{
    public class TourService : ITourService
    {
        private readonly IRepository<Tour, Guid> tourRepository;

        public TourService(IRepository<Tour, Guid> tourRepository)
        {
            this.tourRepository = tourRepository;
        }
        public Task<ICollection<AllTourModel>> IndexGetAllOrderedBySecondNameAsync()
        {
            throw new NotImplementedException();
        }
        public Task<DetailsTourModel> GetTourDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<TourViewModel>> GetAllToursAsync()
        {
            var tours = await tourRepository
                .GetAllAttached()
                .Where(t => !t.IsDeleted) // Exclude soft-deleted tours
                .Select(t => new TourViewModel
                    {
                        Id = t.Id,
                        TourName = t.TourName,
                        TourNumber = t.TourNumber
                    })
                .ToListAsync();

            return tours;
        }
        public Task AddTourAsync(AddTourModel model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateTourAsync(EditTourModel model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> SoftDeleteTourAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        
    }
}
