using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Data;
using LKWSpringerApp.Services.Data.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly IDriverService driverService;
        private readonly ITourService tourService;
        public DriverController(IDriverService driverService, ITourService tourService)
        {
            this.driverService = driverService;
            this.tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ICollection<AllDriverModel> drivers =
                 await driverService.IndexGetAllOrderedBySecondNameAsync();

            return View(drivers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var driver = await driverService.GetDriverDetailsByIdAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var model = new AddDriverModel
            {
                Tours = await tourService.GetAllToursAsync() // Fetch available tours
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddDriverModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tours = await tourService.GetAllToursAsync(); // Reload tours on validation failure
                return View(model);
            }

            try
            {
                await driverService.AddDriverAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName ?? string.Empty, ex.Message);
                model.Tours = await tourService.GetAllToursAsync(); // Reload tours
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await driverService.GetDriverDetailsByIdAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            var model = new EditDriverModel
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                SecondName = driver.SecondName,
                BirthDate = driver.BirthDate,
                StartDate = driver.StartDate,
                PhoneNumber = driver.PhoneNumber,
                Springerdriver = driver.Springerdriver,
                Stammdriver = driver.Stammdriver,
                Tours = driver.Tours,
                SelectedTourIds = driver.Tours.Select(t => t.Id).ToList() // Populate selected tour IDs
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditDriverModel model)
        {
            if (id != model.Id)
            {
                return NotFound(); // Ensure the IDs match
            }

            if (!ModelState.IsValid)
            {
                return View(model); // Return the form with validation errors
            }

            var result = await driverService.UpdateDriverAsync(model);

            if (!result)
            {
                return NotFound(); // Return 404 if the driver wasn't found or update failed
            }

            return RedirectToAction(nameof(Index)); // Redirect to the driver list after saving
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var driver = await driverService.GetDriverDetailsByIdAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            var model = new DeleteDriverModel
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                SecondName = driver.SecondName
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await driverService.SoftDeleteDriverAsync(id);

            if (!result)
            {
                return NotFound(); // Return 404 if the driver doesn't exist or is already deleted
            }

            return RedirectToAction(nameof(Index)); // Redirect to the list of drivers
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTour(Guid driverId, Guid tourId)
        {
            // Call a service method to remove the tour association
            var result = await driverService.RemoveDriverFromTourAsync(driverId, tourId);

            if (!result)
            {
                return NotFound(); // Return 404 if the association wasn't found
            }

            return RedirectToAction(nameof(Edit), new { id = driverId }); // Redirect back to the edit page
        }
    }
}
