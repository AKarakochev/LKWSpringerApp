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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var clients = await clientService.IndexGetAllOrderedByNameAsync(pageIndex, pageSize);
                return View(clients);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred while loading clients.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View(new AddClientModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

            try
            {
                var updated = await clientService.UpdateClientAsync(model);

                if (!updated)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Client updated successfully.";
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client ID.");
            }

            try
            {
                var result = await clientService.SoftDeleteClientAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Client deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "A database error occurred while deleting the client.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
