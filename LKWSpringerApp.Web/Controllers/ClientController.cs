using LKWSpringerApp.Web.ViewModels.Client;
using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;
using static LKWSpringerApp.Common.EntityValidationConstants.Client;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Client;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly LkwSpringerDbContext context;

        public ClientController(LkwSpringerDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = await context.Clients
                .Where(c => !c.IsDeleted)
                .Select(c => new ClientViewModel
                {
                        Id = c.Id,
                        Name = c.Name,
                        ClientNumber = c.ClientNumber,
                        Address = c.Address,
                        PhoneNumber = c.PhoneNumber,
                        DeliveryDescription = c.DeliveryDescription,
                        DeliveryTime = c.DeliveryTime,
                        AddressUrl = c.AddressUrl,
                        Images = c.Images.Select(img => new ClientImageModel
                        {
                            Id = img.Id,
                            ImageUrl = img.ImageUrl,
                            VideoUrl = img.VideoUrl,
                            Description = img.Description
                        }).ToList()
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = await context.Clients
                .Where(c => c.Id == id && !c.IsDeleted)
                .Select(c => new DetailsClientModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClientNumber = c.ClientNumber,
                    Address = c.Address,
                    AddressUrl = c.AddressUrl,
                    PhoneNumber = c.PhoneNumber,
                    DeliveryDescription = c.DeliveryDescription,
                    DeliveryTime = c.DeliveryTime,
                    Images = c.Images.Select(img => new ClientImageModel
                    {
                        Id = img.Id,
                        ImageUrl = img.ImageUrl,
                        VideoUrl = img.VideoUrl,
                        Description = img.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddClientModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newClient = new Client
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ClientNumber = model.ClientNumber ?? 0,
                Address = model.Address,
                AddressUrl = model.AddressUrl,
                PhoneNumber = model.PhoneNumber,
                DeliveryDescription = model.DeliveryDescription,
                DeliveryTime = model.DeliveryTime
            };

            context.Clients.Add(newClient);
            await context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirect to client list or confirmation page
        }
    

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = await context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var model = new EditClientModel
            {
                Id = client.Id,
                Name = client.Name,
                ClientNumber = client.ClientNumber,
                Address = client.Address,
                AddressUrl = client.AddressUrl,
                PhoneNumber = client.PhoneNumber,
                DeliveryDescription = client.DeliveryDescription,
                DeliveryTime = client.DeliveryTime
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditClientModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = await context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            // Update client details
            client.Name = model.Name;
            client.ClientNumber = model.ClientNumber;
            client.Address = model.Address;
            client.AddressUrl = model.AddressUrl;
            client.PhoneNumber = model.PhoneNumber;
            client.DeliveryDescription = model.DeliveryDescription;
            client.DeliveryTime = model.DeliveryTime;

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await context.Clients
                .Where(c => c.Id == id && !c.IsDeleted)
                .Select(c => new DeleteClientModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClientNumber = c.ClientNumber
                })
                .FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = await context.Clients.FindAsync(id);
            if (client == null || client.IsDeleted)
            {
                return NotFound();
            }

            client.IsDeleted = true;
            context.Update(client);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirect to the list of clients
        }
    }
}
