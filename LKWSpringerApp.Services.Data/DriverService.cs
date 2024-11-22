using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Driver;
using static LKWSpringerApp.Common.EntityValidationConstants.Driver;

using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LKWSpringerApp.Services.Data
{
    public class DriverService : IDriverService
    {
        private readonly IRepository<Driver, Guid> driverRepository;

        public DriverService(IRepository<Driver, Guid> driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public async Task<ICollection<AllDriverModel>> IndexGetAllOrderedBySecondNameAsync()
        {
            var drivers = await this.driverRepository
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
                .OrderBy(d => d.SecondName)
                .ToListAsync();

            return drivers;
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
            // Retrieve the existing driver from the repository
            var driver = await driverRepository.GetByIdAsync(model.Id);

            if (driver == null || driver.IsDeleted)
            {
                return false; // Driver not found or soft-deleted
            }

            // Update driver properties
            driver.FirstName = model.FirstName;
            driver.SecondName = model.SecondName;
            driver.BirthDate = DateTime.ParseExact(model.BirthDate, DriverBirthDateFormat, CultureInfo.InvariantCulture);
            driver.StartDate = DateTime.ParseExact(model.StartDate, DriverStartDateFormat, CultureInfo.InvariantCulture);
            driver.PhoneNumber = model.PhoneNumber;
            driver.Springerdriver = model.Springerdriver;
            driver.Stammdriver = model.Stammdriver;

            // Save changes using the repository
            return await driverRepository.UpdateAsync(driver);
        }
        public async Task<bool> SoftDeleteDriverAsync(Guid id)
        {
            // Retrieve the driver from the repository
            var driver = await driverRepository.GetByIdAsync(id);

            if (driver == null || driver.IsDeleted)
            {
                return false; // Return false if the driver doesn't exist or is already soft-deleted
            }

            // Mark the driver as deleted
            driver.IsDeleted = true;

            // Update the driver in the repository
            return await driverRepository.UpdateAsync(driver);
        }

        
    }
}
