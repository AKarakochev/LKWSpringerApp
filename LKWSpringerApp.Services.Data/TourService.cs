using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Tour;
using LKWSpringerApp.Web.ViewModels.TourModels;

using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Services.Data
{
    public class TourService : ITourService
    {
        private readonly IRepository<Tour, Guid> tourRepository;
        private readonly IRepository<DriverTour, Guid> driverTourRepository;
        private readonly LkwSpringerDbContext dbContext;

        public TourService(IRepository<Tour, Guid> tourRepository, 
            IRepository<DriverTour, Guid> driverTourRepository,
            LkwSpringerDbContext dbContext)
        {
            this.tourRepository = tourRepository;
            this.driverTourRepository = driverTourRepository;
            this.dbContext = dbContext;
        }
        public async Task<PaginatedList<AllTourModel>> IndexGetAllOrderedByTourNameAsync(int pageIndex, int pageSize)
        {
            var query = tourRepository
                .GetAllAttached()
                .Where(t => !t.IsDeleted)
                .Select(t => new AllTourModel
                {
                    Id = t.Id,
                    TourNumber = t.TourNumber,
                    TourName = t.TourName,
                    IsDeleted = t.IsDeleted,
                    Clients = t.ToursClients
                        .Where(tc => !tc.Client.IsDeleted)
                        .Select(tc => new ClientModel
                        {
                            Id = tc.Client.Id,
                            Name = tc.Client.Name
                        })
                        .ToList()
                })
                .OrderBy(t => t.TourName);

            return await PaginatedList<AllTourModel>.CreateAsync(query, pageIndex, pageSize);
        }
        public async Task<DetailsTourModel> GetTourDetailsByIdAsync(Guid id)
        {
            var tour = await tourRepository
                .GetAllAttached()
                .Where(t => t.Id == id && !t.IsDeleted)
                .Select(t => new DetailsTourModel
                {
                    Id = t.Id,
                    TourNumber = t.TourNumber,
                    TourName = t.TourName,
                    Clients = t.ToursClients
                        .Where(tc => !tc.Client.IsDeleted)
                        .Select(tc => new ClientModelDetails
                        {
                            Id = tc.Client.Id,
                            Name = tc.Client.Name
                        })
                        .ToList(),
                    Drivers = t.DriverTours
                        .Select(dt => new DriverModel
                        {
                            Id = dt.Driver.Id,
                            FirstName = dt.Driver.FirstName,
                            SecondName = dt.Driver.SecondName
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return tour;
        }
        public async Task<List<TourViewModel>> GetAllToursAsync()
        {
            var tours = await tourRepository
                .GetAllAttached()
                .Where(t => !t.IsDeleted)
                .Select(t => new TourViewModel
                    {
                        Id = t.Id,
                        TourName = t.TourName,
                        TourNumber = t.TourNumber
                    })
                .ToListAsync();

            return tours;
        }
        public async Task AddTourAsync(AddTourModel model)
        {
            bool tourExists = await tourRepository
                .GetAllAttached()
                .AnyAsync(t => !t.IsDeleted &&
                   (t.TourName == model.TourName || t.TourNumber == model.TourNumber));

            if (tourExists)
            {
                throw new ArgumentException("A tour with the same name or number already exists.");
            }

            var newTour = new Tour
            {
                Id = Guid.NewGuid(),
                TourName = model.TourName,
                TourNumber = model.TourNumber,
                IsDeleted = false
            };

            await tourRepository.AddAsync(newTour);

            foreach (var driverId in model.SelectedDriverIds)
            {
                await driverTourRepository.AddAsync(new DriverTour
                {
                    DriverId = driverId,
                    TourId = newTour.Id
                });
            }

            await tourRepository.SaveChangesAsync();
        }
        public async Task<bool> UpdateTourAsync(EditTourModel model)
        {
            var tour = await tourRepository
                .GetAllAttached()
                .Include(t => t.DriverTours)
                .FirstOrDefaultAsync(t => t.Id == model.Id && !t.IsDeleted);

            if (tour == null)
            {
                return false;
            }

            tour.TourName = model.TourName;
            tour.TourNumber = model.TourNumber;

            var currentDriverIds = tour.DriverTours.Select(dt => dt.DriverId).ToList();
            var selectedDriverIds = model.SelectedDriverIds;

            foreach (var driverId in selectedDriverIds.Except(currentDriverIds))
            {
                await driverTourRepository.AddAsync(new DriverTour
                {
                    DriverId = driverId,
                    TourId = tour.Id
                });
            }

            foreach (var driverId in currentDriverIds.Except(selectedDriverIds))
            {
                var driverTour = tour.DriverTours.FirstOrDefault(dt => dt.DriverId == driverId);
                if (driverTour != null)
                {
                    driverTourRepository.Delete(driverTour);
                }
            }

            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SoftDeleteTourAsync(Guid id)
        {
            var tour = await tourRepository.GetByIdAsync(id);

            if (tour == null || tour.IsDeleted)
            {
                return false;
            }

            tour.IsDeleted = true;

            await dbContext.SaveChangesAsync();
            return true;
        }   
    }
}
