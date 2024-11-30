using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.ClientImage;
using static LKWSpringerApp.Common.ErrorMessagesConstants.ClientImage;
using static LKWSpringerApp.Common.SuccessMessagesConstants.ClientImage;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


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
                TempData["ErrorMessage"] = ClientImageTryAgainErrorMessage;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(ClientImageInvalidIdErrorMessage);
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
                Clients = clients.Select(c => new SelectListItem
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
            if (!ModelState.IsValid)
            {
                var clients = await clientImageService.GetAllClientsAsync();
                model.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.ClientName
                }).ToList();

                return View(model);
            }

            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var allowedVideoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };

            if (model.ImageFile != null && !allowedImageExtensions.Contains(Path.GetExtension(model.ImageFile.FileName).ToLower()))
            {
                ModelState.AddModelError("ImageFile", ClientImageInvalidImageFormatErrorMessage);
            }

            if (model.VideoFile != null && !allowedVideoExtensions.Contains(Path.GetExtension(model.VideoFile.FileName).ToLower()))
            {
                ModelState.AddModelError("VideoFile", ClientImageInvalidVideoFormatErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                var clients = await clientImageService.GetAllClientsAsync();
                model.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.ClientName
                }).ToList();

                return View(model);
            }

            try
            {
                await clientImageService.AddClientImageAsync(model);
                TempData["SuccessMessage"] = ClientImageAddedSuccessMessage;
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
                return BadRequest(ClientImageInvalidIdErrorMessage);
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
        public async Task<IActionResult> Edit(Guid id, EditClientImageModel model, IFormFile? newImageFile,IFormFile? newVideoFile)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return BadRequest(ClientImageInvalidIdErrorMessage);
            }

            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var allowedVideoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };

            if (newImageFile != null && !allowedImageExtensions.Contains(Path.GetExtension(newImageFile.FileName).ToLower()))
            {
                ModelState.AddModelError("NewImageFile", ClientImageInvalidImageFormatErrorMessage);
            }

            if (newVideoFile != null && !allowedVideoExtensions.Contains(Path.GetExtension(newVideoFile.FileName).ToLower()))
            {
                ModelState.AddModelError("NewVideoFile", ClientImageInvalidVideoFormatErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var updateResult = await clientImageService.UpdateClientImageAsync(id, model, newImageFile,newVideoFile);

                if (!updateResult)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = ClientImageUpdatedSuccessMessage;
                return RedirectToAction("Details", new { id = model.ClientId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, ClientImageTryAgainErrorMessage);
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(ClientImageInvalidIdErrorMessage);
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
                return BadRequest(ClientImageInvalidIdErrorMessage);
            }

            try
            {
                var result = await clientImageService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = ClientImageDeletedSuccessMessage;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = ClientImageTryAgainErrorMessage;
                return RedirectToAction("Index");
            }
        }
    }
}

