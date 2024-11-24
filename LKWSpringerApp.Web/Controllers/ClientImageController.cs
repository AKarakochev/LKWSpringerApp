using Microsoft.AspNetCore.Mvc;
using LKWSpringerApp.Web.ViewModels.ClientImage;
using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Services.Data.Interfaces;

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
            var model = await clientImageService.GetClientImageDetailsByIdAsync(id);

            if (model == null)
            {
                return NotFound(); // Return a 404 page if no data is found
            }

            return View(model); // Pass the model to the view
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
            if (!ModelState.IsValid)
            {
                return View(model); // Return the form with validation errors
            }

            // Update only the changed fields
            var updateResult = await clientImageService.UpdateClientImageAsync(id, model, newImageFile);

            if (!updateResult)
            {
                return NotFound(); // Return 404 if the image doesn't exist
            }

            return RedirectToAction("Details", new { id = model.ClientId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Fetch the client image details using the service
            var image = await clientImageService.GetClientImageDetailsByIdAsync(id);

            if (image == null || !image.MediaFiles.Any())
            {
                return NotFound();
            }

            // Map the first media file from the details model to DeleteClientImageModel
            var mediaFile = image.MediaFiles.First();
            var model = new DeleteClientImageModel
            {
                Id = mediaFile.Id,
                ClientId = image.ClientId,
                ImageUrl = mediaFile.ImageUrl ?? string.Empty,
                Description = mediaFile.Description ?? string.Empty
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await clientImageService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}

