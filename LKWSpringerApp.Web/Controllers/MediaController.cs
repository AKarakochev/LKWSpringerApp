using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Media;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Media;
using static LKWSpringerApp.Common.SuccessMessagesConstants.Media;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
      
        private readonly IMediaService mediaService;
        public MediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var paginatedClients = await mediaService.IndexGetAllOrderedByClientNameAsync(pageIndex, pageSize);
                ViewData["PageSize"] = pageSize;
                return View(paginatedClients);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = MediaTryAgainErrorMessage;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Invalid media ID.";
                return RedirectToAction("Index");
            }

            var mediaDetails = await mediaService.GetClientMediaDetailsByIdAsync(id);

            if (mediaDetails == null)
            {
                TempData["ErrorMessage"] = "Media not found.";
                return RedirectToAction("Index");
            }

            return View(mediaDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Add()
        {
            var clients = await mediaService.GetAllClientsMediaAsync();
            var model = new AddMediaModel
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
        public async Task<IActionResult> Add(AddMediaModel model)
        {
            if (!ModelState.IsValid)
            {
                var clients = await mediaService.GetAllClientsMediaAsync();
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
                ModelState.AddModelError("ImageFile", MediaInvalidImageFormatErrorMessage);
            }

            if (model.VideoFile != null && !allowedVideoExtensions.Contains(Path.GetExtension(model.VideoFile.FileName).ToLower()))
            {
                ModelState.AddModelError("VideoFile", MediaInvalidVideoFormatErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                var clients = await mediaService.GetAllClientsMediaAsync();
                model.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.ClientName
                }).ToList();

                return View(model);
            }

            try
            {
                await mediaService.AddClientMediaAsync(model);
                TempData["SuccessMessage"] = MediaAddedSuccessMessage;
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
                return BadRequest(MediaInvalidIdErrorMessage);
            }

            var model = await mediaService.GetSingleMediaFileByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditMediaModel model, IFormFile? newImageFile,IFormFile? newVideoFile)
        {
            if (id == Guid.Empty || id != model.Id)
            {
                return BadRequest(MediaInvalidIdErrorMessage);
            }

            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var allowedVideoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };

            if (newImageFile != null && !allowedImageExtensions.Contains(Path.GetExtension(newImageFile.FileName).ToLower()))
            {
                ModelState.AddModelError("NewImageFile", MediaInvalidImageFormatErrorMessage);
            }

            if (newVideoFile != null && !allowedVideoExtensions.Contains(Path.GetExtension(newVideoFile.FileName).ToLower()))
            {
                ModelState.AddModelError("NewVideoFile", MediaInvalidVideoFormatErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var updateResult = await mediaService.UpdateClientMediaAsync(id, model, newImageFile,newVideoFile);

                if (!updateResult)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = MediaUpdatedSuccessMessage;
                return RedirectToAction("Details", new { id = model.ClientId });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, MediaTryAgainErrorMessage);
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(MediaInvalidIdErrorMessage);
            }

            var model = await mediaService.GetClientMediaByIdAsync(id);

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
                return BadRequest(MediaInvalidIdErrorMessage);
            }

            try
            {
                var result = await mediaService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = MediaDeletedSuccessMessage;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = MediaTryAgainErrorMessage;
                return RedirectToAction("Index");
            }
        }
    }
}

