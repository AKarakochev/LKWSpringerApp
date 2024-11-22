using LKWSpringerApp.Web.ViewModels.Client;
using LKWSpringerApp.Services.Data.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Add(AddClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await clientService.AddClientAsync(model);

            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
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

            var updated = await clientService.UpdateClientAsync(model);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await clientService.GetClientDetailsByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            // Map to DeleteClientModel
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
            var result = await clientService.SoftDeleteClientAsync(id);

            if (!result)
            {
                return NotFound(); // Return 404 if the client was not found or is not deletable
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
