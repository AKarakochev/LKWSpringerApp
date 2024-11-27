using LKWSpringerApp.Web.ViewModels.Client;
using LKWSpringerApp.Services.Data.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ICollection<AllClientModel> clients = 
                await clientService.IndexGetAllOrderedByNameAsync();

            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client ID.");
            }

            var client = await clientService.GetClientDetailsByIdAsync(id);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,ClientNumber,Address,AddressUrl,PhoneNumber,DeliveryDescription,DeliveryTime")] AddClientModel model)
        {
            if(!ModelState.IsValid)
    {
                return View(model);
            }

            try
            {
                await clientService.AddClientAsync(model);
                TempData["SuccessMessage"] = "Client added successfully.";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "Database error occurred. Please try again later.");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }
    

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client ID.");
            }

            var client = await clientService.GetClientDetailsByIdAsync(id);

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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ClientNumber,Address,AddressUrl,PhoneNumber,DeliveryDescription,DeliveryTime")] EditClientModel model)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return BadRequest("Invalid client ID.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updated = await clientService.UpdateClientAsync(model);

            if (!updated)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Client updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client ID.");
            }

            var client = await clientService.GetClientDetailsByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var model = new DeleteClientModel
            {
                Id = client.Id,
                Name = client.Name,
                ClientNumber = client.ClientNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client ID.");
            }

            var result = await clientService.SoftDeleteClientAsync(id);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Client deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
