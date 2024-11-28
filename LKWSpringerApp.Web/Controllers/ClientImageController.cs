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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var paginatedClients = await clientImageService.IndexGetAllOrderedByClientNameAsync(pageIndex, pageSize);
                ViewData["PageSize"] = pageSize;
                return View(paginatedClients);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred while loading client media.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Add()
        {
            var clients = await clientImageService.GetAllClientsAsync(); // Use existing service method
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
        [Authorize(Roles = "Admin,User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddClientImageModel model)
        {
            if (!ModelState.IsValid || model.ImageFile == null)
            {
                var clients = await clientImageService.GetAllClientsAsync();
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

            try
            {
                var updateResult = await clientImageService.UpdateClientImageAsync(id, model, newImageFile);

                if (!updateResult)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Client image updated successfully.";
                return RedirectToAction("Details", new { id = model.ClientId });
            }
            catch
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid client image ID.");
            }

            try
            {
                var result = await clientImageService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Client image deleted successfully.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the client image. Please try again later.";
                return RedirectToAction("Index");
            }
        }
    }
}

