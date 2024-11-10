using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Authorization;

using LKWSpringerApp.Web.ViewModels.Driver;
using LKWSpringerApp.Data;
using static LKWSpringerApp.Common.EntityValidationConstants.Tour;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;
using LKWSpringerApp.Web.ViewModels.TourModels;

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
                .ToListAsync();

            return View(tours);
        }
    }
}
