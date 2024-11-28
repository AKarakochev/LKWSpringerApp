using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Web.ViewModels.Tour;
using LKWSpringerApp.Web.ViewModels.TourModels;
using LKWSpringerApp.Data;
using static LKWSpringerApp.Common.EntityValidationConstants.Driver;

using Microsoft.EntityFrameworkCore;
using System.Globalization;
using LKWSpringerApp.Services.Data.Helpers;

namespace LKWSpringerApp.Services.Data
{
    public class DriverService : IDriverService
    {
        private readonly IRepository<Driver, Guid> driverRepository;
        private readonly IRepository<DriverTour, Guid> driverTourRepository;
        private readonly LkwSpringerDbContext dbContext;

        public DriverService(
            IRepository<Driver, Guid> driverRepository,
            IRepository<DriverTour, Guid> driverTourRepository,
            LkwSpringerDbContext dbContext)
        {
            this.driverRepository = driverRepository;
            this.driverTourRepository = driverTourRepository;
            this.dbContext = dbContext;
        }

        public async Task<PaginatedList<AllDriverModel>> IndexGetAllOrderedBySecondNameAsync(int pageIndex, int pageSize)
        {
            var query = driverRepository
                .GetAllAttached()
                .Where(d => !d.IsDeleted)
                .Select(d => new AllDriverModel
                {
                    Id = d.Id.ToString(),
                    FirstName = d.FirstName,
                    SecondName = d.SecondName,
                    PhoneNumber = d.PhoneNumber,
                    Springerdriver = d.Springerdriver,
                    Stammdriver = d.Stammdriver
                })
                .OrderBy(d => d.SecondName);

            return await PaginatedList<AllDriverModel>.CreateAsync(query, pageIndex, pageSize);
        }
        public async Task<DetailsDriverModel> GetDriverDetailsByIdAsync(Guid id)
        {
            var driver = await this.driverRepository
                .GetAllAttached()
                .Include(d => d.DriverTours)
                .ThenInclude(dt => dt.Tour)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                return null;
            }

            var model = new DetailsDriverModel
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                SecondName = driver.SecondName,
                BirthDate = string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", driver.BirthDate),
                StartDate = string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", driver.StartDate),
                PhoneNumber = driver.PhoneNumber,
                Springerdriver = driver.Springerdriver,
                Stammdriver = driver.Stammdriver,
                Tours = driver.DriverTours.Select(dt => new TourViewModel
                {
                    Id = dt.Tour.Id,
                    TourName = dt.Tour.TourName,
                    TourNumber = dt.Tour.TourNumber
                }).ToList()
            };

            return model;
        }
        public async Task<List<DriverModel>> GetAllDriversAsync()
        {
            return await driverRepository.GetAllAttached()
                .Where(d => !d.IsDeleted)
                .Select(d => new DriverModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    SecondName = d.SecondName
                })
                .ToListAsync();
        }
        public async Task AddDriverAsync(AddDriverModel model)
        {
            if (!DateTime.TryParseExact(model.BirthDate, DriverBirthDateFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out DateTime driverBirthDate))
            {
                throw new ArgumentException("Invalid Birth Date format.", nameof(model.BirthDate));
            }

            int age = DateTime.Now.Year - driverBirthDate.Year;
            if (driverBirthDate > DateTime.Now.AddYears(-age)) age--;
            if (age < 18)
            {
                throw new ArgumentException("Driver must be at least 18 years old.", nameof(model.BirthDate));
            }

            if (!DateTime.TryParseExact(model.StartDate, DriverStartDateFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out DateTime driverStartDate))
            {
                throw new ArgumentException("Invalid Start Date format.", nameof(model.StartDate));
            }

            var newDriver = new Driver
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                BirthDate = driverBirthDate,
                StartDate = driverStartDate,
                PhoneNumber = model.PhoneNumber,
                Springerdriver = model.Springerdriver,
                Stammdriver = model.Stammdriver
            };

            await driverRepository.AddAsync(newDriver);
        }
        public async Task<bool> UpdateDriverAsync(EditDriverModel model)
        {
            var driver = await driverRepository.GetByIdAsync(model.Id);

            if (driver == null || driver.IsDeleted)
            {
                return false;
            }
            
            driver.FirstName = model.FirstName;
            driver.SecondName = model.SecondName;
            driver.BirthDate = DateTime.ParseExact(model.BirthDate, DriverBirthDateFormat, CultureInfo.InvariantCulture);
            driver.StartDate = DateTime.ParseExact(model.StartDate, DriverStartDateFormat, CultureInfo.InvariantCulture);
            driver.PhoneNumber = model.PhoneNumber;
            driver.Springerdriver = model.Springerdriver;
            driver.Stammdriver = model.Stammdriver;

            var currentTourIds = driver.DriverTours.Select(dt => dt.TourId).ToList();

            var selectedTourIds = model.SelectedTourIds;

            foreach (var tourId in selectedTourIds.Except(currentTourIds))
            {
                await driverTourRepository.AddAsync(new DriverTour
                {
                    DriverId = driver.Id,
                    TourId = tourId
                });
            }

            foreach (var tourId in currentTourIds.Except(selectedTourIds))
            {
                await RemoveDriverFromTourAsync(driver.Id, tourId);
            }
            
            return await driverRepository.UpdateAsync(driver);
        }
        public async Task<bool> SoftDeleteDriverAsync(Guid id)
        {
            var driver = await driverRepository.GetByIdAsync(id);

            if (driver == null || driver.IsDeleted)
            {
                return false;
            }

            driver.IsDeleted = true;

            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveDriverFromTourAsync(Guid driverId, Guid tourId)
        {
            var driverTour = await driverTourRepository
        .GetAllAttached()
        .FirstOrDefaultAsync(dt => dt.DriverId == driverId && dt.TourId == tourId);

            if (driverTour == null)
            {
                return false;
            }

            driverTourRepository.Delete(driverTour);

            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
