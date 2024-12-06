using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Services.Data.Interfaces;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Driver;
using static LKWSpringerApp.Common.SuccessMessagesConstants.Driver;

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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 15)
        {
            ViewData["BackController"] = "Driver";
            ViewData["BackAction"] = "Index";

            try
            {
                var paginatedDrivers = await driverService.IndexGetAllOrderedBySecondNameAsync(pageIndex, pageSize);
                ViewData["PageSize"] = pageSize;
                return View(paginatedDrivers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = DriverTryAgainErrorMessage;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(DriverInvalidIdErrorMessage);
            }

            var driver = await driverService.GetDriverDetailsByIdAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {

            var model = new AddDriverModel
            {
                Tours = await tourService.GetAllToursAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddDriverModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tours = await tourService.GetAllToursAsync();
                return View(model);
            }

            try
            {
                await driverService.AddDriverAsync(model);
                TempData["SuccessMessage"] = DriverAddedSuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName ?? string.Empty, ex.Message);
                model.Tours = await tourService.GetAllToursAsync();
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(DriverInvalidIdErrorMessage);
            }

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
                SelectedTourIds = driver.Tours.Select(t => t.Id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditDriverModel model)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return BadRequest(DriverInvalidIdErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await driverService.UpdateDriverAsync(model);

                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = DriverUpdatedSuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, DriverTryAgainErrorMessage);
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(DriverInvalidIdErrorMessage);
            }

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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(DriverInvalidIdErrorMessage);
            }

            try
            {
                var result = await driverService.SoftDeleteDriverAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = DriverDeletedSuccessMessage;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = DriverTryAgainErrorMessage;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTour(Guid driverId, Guid tourId)
        {
            if (driverId == Guid.Empty || tourId == Guid.Empty)
            {
                return BadRequest(DriverOrTourInvalidIdErrorMessage);
            }
            
            var result = await driverService.RemoveDriverFromTourAsync(driverId, tourId);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = DriverTourDeletedSuccessMessage;
            return RedirectToAction(nameof(Edit), new { id = driverId });
        }
    }
}
