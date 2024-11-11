using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Data;
using static LKWSpringerApp.Common.EntityValidationConstants.Driver;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Driver;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Authorization;



namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly LkwSpringerDbContext context;

        public DriverController(LkwSpringerDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var drivers = await context.Drivers
                    .Where(d => d.IsDeleted == false)
                    .Include(d => d.DriverTours)
                    .Select(d => new AllDriverModel()
                    {
                        Id = d.Id.ToString(),
                        FirstName = d.FirstName,
                        SecondName = d.SecondName,
                        PhoneNumber = d.PhoneNumber,
                        TourNames = d.DriverTours.Select(dt => dt.Tour.TourName).ToList()
                    })
                    .AsNoTracking()
                    .ToListAsync();

            return View(drivers);
        }


        [HttpGet]
        public async Task<IActionResult> AddDriver()
        {
            var model = new AddDriverModel();
            model.Tours = await context.Tours.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver(AddDriverModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            DateTime driverBirthDate;

            if (DateTime.TryParseExact(model.BirthDate, DriverBirthDateFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out driverBirthDate) == false)
            {
                ModelState.AddModelError(nameof(model.BirthDate), DriverBirthDateErrorMessage);
                model.Tours = await context.Tours.ToListAsync();

                return View(model);
            }

            int age = DateTime.Now.Year - driverBirthDate.Year;
            if (driverBirthDate > DateTime.Now.AddYears(-age)) age--; // Adjust if birthdate hasn't occurred this year

            if (age < 18)
            {
                ModelState.AddModelError(nameof(model.BirthDate), "The driver must be at least 18 years old.");
                model.Tours = await context.Tours.ToListAsync();
                return View(model);
            }

            DateTime driverStartDate;

            if (DateTime.TryParseExact(model.StartDate, DriverStartDateFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out driverStartDate) == false)
            {
                ModelState.AddModelError(nameof(model.StartDate), DriverStartDateErrorMessage);
                model.Tours = await context.Tours.ToListAsync();

                return View(model);
            }

            Driver newDriver = new Driver
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                BirthDate = driverBirthDate,
                StartDate = driverStartDate,
                PhoneNumber = model.PhoneNumber,
                Springerdriver = model.Springerdriver,
                Stammdriver = model.Stammdriver
            };

            await context.Drivers.AddAsync(newDriver);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var driver = await context.Drivers
                .Include(d => d.DriverTours)
                .ThenInclude(dt => dt.Tour)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                return NotFound();
            }

            var model = new DetailsDriverModel
            {
                Id = id,
                FirstName = driver.FirstName,
                SecondName = driver.SecondName,
                BirthDate = driver.BirthDate.ToString("dd/MM/yyyy"),
                StartDate = driver.StartDate.ToString("dd/MM/yyyy"),
                PhoneNumber = driver.PhoneNumber,
                Springerdriver = driver.Springerdriver,
                Stammdriver = driver.Stammdriver,
                Tours = driver.DriverTours.Select(dt => dt.Tour.TourName).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await context.Drivers
                .Include(d => d.DriverTours)
                .ThenInclude(dt => dt.Tour)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                return NotFound();
            }

            var model = new EditDriverModel
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                SecondName = driver.SecondName,
                BirthDate = driver.BirthDate.ToString(DriverBirthDateFormat),
                StartDate = driver.StartDate.ToString(DriverStartDateFormat),
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditDriverModel model)
        {
            if (id != model.Id)
            {
                return NotFound(); // Make sure the IDs match
            }

            if (!ModelState.IsValid)
            {
                return View(model); // If validation fails, return the form with errors
            }

            var driver = await context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound(); // If the driver doesn't exist, return 404
            }

            // Update the driver's properties
            driver.FirstName = model.FirstName;
            driver.SecondName = model.SecondName;
            driver.BirthDate = DateTime.ParseExact(model.BirthDate, DriverBirthDateFormat, CultureInfo.InvariantCulture);
            driver.StartDate = DateTime.ParseExact(model.StartDate, DriverStartDateFormat, CultureInfo.InvariantCulture);
            driver.PhoneNumber = model.PhoneNumber;
            driver.Springerdriver = model.Springerdriver;
            driver.Stammdriver = model.Stammdriver;

            // Save the changes
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirect to the driver list after saving
        }

        public async Task<IActionResult> DeleteTour(Guid driverId, Guid tourId)
        {
            var driverTour = await context.DriverTours
        .FirstOrDefaultAsync(dt => dt.DriverId == driverId && dt.TourId == tourId);

            if (driverTour == null)
            {
                return NotFound();
            }

            var driverToursCount = await context.DriverTours
                .CountAsync(dt => dt.DriverId == driverId);

            if (driverToursCount <= 1)
            {
                TempData["Error"] = "A driver must have at least one tour assigned.";
                return RedirectToAction("Edit", new { id = driverId });
            }

            context.DriverTours.Remove(driverTour);
            await context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = driverId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var driver = await context.Drivers
                .Where(d => d.Id == id && !d.IsDeleted)
                .Select(d => new DeleteDriverModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    SecondName = d.SecondName
                })
                .FirstOrDefaultAsync();

            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var driver = await context.Drivers.FindAsync(id);
            if (driver == null || driver.IsDeleted)
            {
                return NotFound();
            }

            driver.IsDeleted = true;
            context.Update(driver);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirect to the list of drivers
        }
    }
}
