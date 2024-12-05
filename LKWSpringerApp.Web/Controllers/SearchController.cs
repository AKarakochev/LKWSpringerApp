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
                return View(Enumerable.Empty<dynamic>());
            }

            var drivers = await _context.Drivers
                .Where(d => !d.IsDeleted && (d.FirstName.Contains(searchQuery) || d.SecondName.Contains(searchQuery)))
                .Select(d => new
                {
                    Type = "Driver",
                    Name = $"{d.SecondName}, {d.FirstName} ",
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

            var media = await _context.Media
                .Include(m => m.Client)
                .Where(m => m.Description.Contains(searchQuery) || m.Client.Name.Contains(searchQuery))
                .Select(m => new
                {
                    Type = "Media",
                    Name = $"{m.Client.Name} - {m.Description}",
                    Link = Url.Action("Details", "Media", new { id = m.Id }) ?? "#"
                })
                .ToListAsync();

            var pinBoards = await _context.PinBoards
                .Include(pb => pb.Driver)
                .Where(pb => pb.Driver.FirstName.Contains(searchQuery)
                             || pb.Driver.SecondName.Contains(searchQuery)
                             || pb.News.Contains(searchQuery)
                             || pb.ImportantNews.Contains(searchQuery))
                .Select(pb => new
                {
                    Type = "Pin Board",
                    Name = $"{pb.Driver.SecondName}, {pb.Driver.FirstName}",
                    Link = Url.Action("Details", "PinBoard", new { id = pb.DriverId })
                })
                .ToListAsync();

            var results = drivers
                .Union(clients)
                .Union(tours)
                .Union(media)
                .Union(pinBoards)
                .OrderBy(r => r.Type);

            return View(results);
        }
    }
}
