using LKWSpringerApp.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly LkwSpringerDbContext _context;

        public SearchController(LkwSpringerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return View("No Results");
            }

            var drivers = await _context.Drivers
                .Where(d => !d.IsDeleted && (d.FirstName.Contains(searchQuery) || d.SecondName.Contains(searchQuery)))
                .Select(d => new
                {
                    Type = "Driver",
                    Name = $"{d.FirstName} {d.SecondName}",
                    Link = Url.Action("Details", "Driver", new { id = d.Id })
                })
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Name.Contains(searchQuery))
                .Select(c => new
                {
                    Type = "Client",
                    Name = c.Name,
                    Link = Url.Action("Details", "Client", new { id = c.Id })
                })
                .ToListAsync();

            var tours = await _context.Tours
                .Where(t => t.TourName.Contains(searchQuery))
                .Select(t => new
                {
                    Type = "Tour",
                    Name = t.TourName,
                    Link = Url.Action("Details", "Tour", new { id = t.Id })
                })
                .ToListAsync();

            var clientMedia = await _context.ClientImages
                .Include(ci => ci.Client)
                .Where(ci => ci.Description.Contains(searchQuery) || ci.Client.Name.Contains(searchQuery))
                .Select(ci => new
                {
                    Type = "Client Media",
                    Name = $"{ci.Client.Name} - {ci.Description}",
                    Link = Url.Action("Edit", "ClientImage", new { id = ci.Id })
                })
                .ToListAsync();

            var results = drivers
                .Union(clients)
                .Union(tours)
                .Union(clientMedia)
                .OrderBy(r => r.Type);

            return View(results);
        }
    }
}
