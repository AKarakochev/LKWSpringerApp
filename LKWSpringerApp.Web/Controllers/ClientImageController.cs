using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.ClientImage;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class ClientImageController : Controller
    {
      
        private readonly IClientImageService clientImageService;
        public ClientImageController(IClientImageService clientImageService)
        {
            this.clientImageService = clientImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var clients = await clientImageService.IndexGetAllOrderedByClientNameAsync();
            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client image ID.");
            }

            var model = await clientImageService.GetClientImageDetailsByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var clients = await clientImageService.IndexGetAllOrderedByClientNameAsync(); // Use existing service method
            var model = new AddClientImageModel
            {
                Clients = clients.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.ClientName
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddClientImageModel model)
        {
            if (!ModelState.IsValid || model.ImageFile == null)
            {
                var clients = await clientImageService.IndexGetAllOrderedByClientNameAsync();
                model.Clients = clients.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.ClientName
                }).ToList();

                return View(model);
            }

            try
            {
                await clientImageService.AddClientImageAsync(model);
                TempData["SuccessMessage"] = "Client image added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client image ID.");
            }

            var model = await clientImageService.GetSingleMediaFileByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditClientImageModel model, IFormFile? newImageFile)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return BadRequest("Invalid client image ID.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updateResult = await clientImageService.UpdateClientImageAsync(id, model, newImageFile);

            if (!updateResult)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Client image updated successfully.";
            return RedirectToAction("Details", new { id = model.ClientId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client image ID.");
            }

            var model = await clientImageService.GetClientImageByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client image ID.");
            }

            var result = await clientImageService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Client image deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}

