﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Authorization;

using LKWSpringerApp.Data;
using LKWSpringerApp.Web.ViewModels.TourModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using LKWSpringerApp.Web.ViewModels.Tour;


namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        private readonly LkwSpringerDbContext context;

        public TourController(LkwSpringerDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tours = await context.Tours
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
                .OrderBy(t => t.TourName)
                .ToListAsync();

            return View(tours);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var tour = await context.Tours
                .Where(t => t.Id == id && !t.IsDeleted)
                .Select(t => new TourDetailsModel
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
                .Select(d => new DriverModel
                    {   
                        Id = d.Driver.Id,
                        FirstName = d.Driver.FirstName,
                        SecondName = d.Driver.SecondName
                    })
                .ToList()
                    })
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var drivers = await context.Drivers
                .Where(d => !d.IsDeleted)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"{d.FirstName} {d.SecondName}"
                })
                .ToListAsync();

            var model = new AddTourModel
            {
                Drivers = drivers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTourModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload drivers for the dropdown in case of validation errors
                model.Drivers = await context.Drivers
                    .Where(d => !d.IsDeleted)
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.SecondName}"
                    })
                    .ToListAsync();

                return View(model);
            }

            // Check for duplicates
            bool tourExists = await context.Tours.AnyAsync(t =>
                t.TourName == model.TourName || t.TourNumber == model.TourNumber);

            if (tourExists)
            {
                ModelState.AddModelError(string.Empty, "Tour with that name or number already exists!");

                // Reload drivers for the dropdown
                model.Drivers = await context.Drivers
                    .Where(d => !d.IsDeleted)
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.SecondName}"
                    })
                    .ToListAsync();

                return View(model);
            }

            // Create a new Tour
            var newTour = new Tour
            {
                Id = Guid.NewGuid(),
                TourName = model.TourName,
                TourNumber = model.TourNumber,
                IsDeleted = false
            };

            context.Tours.Add(newTour);

            // Add entries to the DriverTour join table for each selected driver
            foreach (var driverId in model.SelectedDriverIds)
            {
                context.DriverTours.Add(new DriverTour
                {
                    DriverId = driverId,
                    TourId = newTour.Id
                });
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tour = await context.Tours
                .Include(t => t.DriverTours)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (tour == null)
            {
                return NotFound();
            }

            var model = new EditTourModel
            {
                Id = tour.Id,
                TourName = tour.TourName,
                TourNumber = tour.TourNumber,
                SelectedDriverIds = tour.DriverTours.Select(dt => dt.DriverId).ToList(),
                Drivers = await context.Drivers
                    .Where(d => !d.IsDeleted)
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.SecondName}"
                    })
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditTourModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                model.Drivers = await context.Drivers
                    .Where(d => !d.IsDeleted)
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.SecondName}"
                    })
                    .ToListAsync();

                return View(model);
            }

            var tour = await context.Tours
                .Include(t => t.DriverTours)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (tour == null)
            {
                return NotFound();
            }

            // Update basic details
            tour.TourName = model.TourName;
            tour.TourNumber = model.TourNumber;

            // Update the drivers associated with the tour
            var currentDriverIds = tour.DriverTours.Select(dt => dt.DriverId).ToList();
            var selectedDriverIds = model.SelectedDriverIds;

            // Add new drivers
            foreach (var driverId in selectedDriverIds.Except(currentDriverIds))
            {
                context.DriverTours.Add(new DriverTour { DriverId = driverId, TourId = tour.Id });
            }

            // Remove unselected drivers
            foreach (var driverId in currentDriverIds.Except(selectedDriverIds))
            {
                var driverTour = tour.DriverTours.FirstOrDefault(dt => dt.DriverId == driverId);
                if (driverTour != null)
                {
                    context.DriverTours.Remove(driverTour);
                }
            }

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tour = await context.Tours
                .Where(t => t.Id == id && !t.IsDeleted)
                .Select(t => new DeleteTourModel
                {
                    Id = t.Id,
                    TourName = t.TourName,
                    TourNumber = t.TourNumber
                })
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tour = await context.Tours.FindAsync(id);

            if (tour == null || tour.IsDeleted)
            {
                return NotFound();
            }

            tour.IsDeleted = true;
            context.Update(tour);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

