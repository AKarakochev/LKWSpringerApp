using LKWSpringerApp.Web.ViewModels.TourModels;
using LKWSpringerApp.Web.ViewModels.Tour;
using LKWSpringerApp.Services.Data.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        private readonly ITourService tourService;
        private readonly IDriverService driverService;
        public TourController(ITourService tourService, IDriverService driverService)
        {
            this.tourService = tourService;
            this.driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tours = await tourService.IndexGetAllOrderedByTourNameAsync();
            
            return View(tours);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid tour ID.");
            }

            var tour = await tourService.GetTourDetailsByIdAsync(id);

            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var drivers = await driverService.GetAllDriversAsync(); // Fetch all drivers
            var model = new AddTourModel
            {
                Drivers = drivers.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.FirstName} {d.SecondName}"
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddTourModel model)
        {
            if (!ModelState.IsValid)
            {
                var drivers = await driverService.GetAllDriversAsync();
                model.Drivers = drivers.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.FirstName} {d.SecondName}"
                }).ToList();

                return View(model);
            }

            try
            {
                await tourService.AddTourAsync(model);
                TempData["SuccessMessage"] = "Tour added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var drivers = await driverService.GetAllDriversAsync();
                model.Drivers = drivers.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.FirstName} {d.SecondName}"
                }).ToList();

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid tour ID.");
            }

            var tour = await tourService.GetTourDetailsByIdAsync(id);

            if (tour == null)
            {
                return NotFound();
            }

            var drivers = await driverService.GetAllDriversAsync();

            var model = new EditTourModel
            {
                Id = tour.Id,
                TourName = tour.TourName,
                TourNumber = tour.TourNumber,
                SelectedDriverIds = tour.Drivers.Select(d => d.Id).ToList(),
                Drivers = drivers.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.FirstName} {d.SecondName}"
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditTourModel model)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return BadRequest("Invalid tour ID.");
            }

            if (!ModelState.IsValid)
            {
                var drivers = await driverService.GetAllDriversAsync();
                model.Drivers = drivers.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.FirstName} {d.SecondName}"
                }).ToList();

                return View(model);
            }

            var result = await tourService.UpdateTourAsync(model);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Tour updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid tour ID.");
            }

            var tour = await tourService.GetTourDetailsByIdAsync(id);

            if (tour == null)
            {
                return NotFound();
            }

            var model = new DeleteTourModel
            {
                Id = tour.Id,
                TourName = tour.TourName,
                TourNumber = tour.TourNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid tour ID.");
            }

            var result = await tourService.SoftDeleteTourAsync(id);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Tour deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}

