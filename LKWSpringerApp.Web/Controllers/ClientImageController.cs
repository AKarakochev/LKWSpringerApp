using Microsoft.AspNetCore.Mvc;
using LKWSpringerApp.Web.ViewModels.ClientImage;
using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using LKWSpringerApp.Web.ViewModels.Client;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class ClientImageController : Controller
    {
            private readonly LkwSpringerDbContext context;

            public ClientImageController(LkwSpringerDbContext _context)
            {
                context = _context;
            }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var images = await context.ClientImages
                .Include(img => img.Client)
                .Select(img => new AllClientImageModel
                {
                    Id = img.Id,
                    ClientName = img.Client.Name,
                    ImageUrl = img.ImageUrl,
                    Description = img.Description
                })
                .OrderBy(img => img.ClientName)
                .ToListAsync();

            return View(images);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var clients = await context.Clients
                .Where(c => !c.IsDeleted)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            var model = new AddClientImageModel
            {
                Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClientImageModel model)
        {
            if (!ModelState.IsValid || model.ImageFile == null)
            {
                return View(model);
            }

            // Get client information
            var client = await context.Clients.FindAsync(model.ClientId);
            if (client == null || client.IsDeleted)
            {
                return NotFound("Client not found or deleted.");
            }

            // Sanitize client name for folder naming
            var sanitizedClientName = client.Name.ToLower().Replace(" ", "_");
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clients", sanitizedClientName);

            // Ensure the client-specific folder exists
            Directory.CreateDirectory(uploadPath);

            // Generate unique file name
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Save the uploaded file to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            // Save the relative path to the database
            var image = new ClientImage
            {
                Id = Guid.NewGuid(),
                ClientId = model.ClientId,
                ImageUrl = $"images/clients/{sanitizedClientName}/{fileName}", // Relative URL
                VideoUrl = model.VideoUrl,
                Description = model.Description
            };

            context.ClientImages.Add(image);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Client", new { id = model.ClientId });
        }

        [HttpGet]
            public async Task<IActionResult> Edit(Guid id)
            {
                var image = await context.ClientImages.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                var model = new EditClientImageModel
                {
                    Id = image.Id,
                    ClientId = image.ClientId,
                    ImageUrl = image.ImageUrl,
                    VideoUrl = image.VideoUrl,
                    Description = image.Description
                };

                return View(model);
            }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditClientImageModel model, IFormFile newImageFile)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var image = await context.ClientImages.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            // If a new file is uploaded, replace the existing file
            if (newImageFile != null)
            {
                var client = await context.Clients.FindAsync(image.ClientId);
                if (client == null)
                {
                    return NotFound("Client not found.");
                }

                var sanitizedClientName = client.Name.ToLower().Replace(" ", "_");
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clients", sanitizedClientName);

                // Ensure the folder exists
                Directory.CreateDirectory(uploadPath);

                // Generate a new unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(newImageFile.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Save the new file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(fileStream);
                }

                // Delete the old file
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                // Update the image URL
                image.ImageUrl = $"images/clients/{sanitizedClientName}/{fileName}";
            }

            // Update other properties
            image.VideoUrl = model.VideoUrl;
            image.Description = model.Description;

            context.Update(image);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Client", new { id = model.ClientId });
        }

        [HttpGet]
            public async Task<IActionResult> Delete(Guid id)
            {
                var image = await context.ClientImages
                    .Include(ci => ci.Client)
                    .FirstOrDefaultAsync(ci => ci.Id == id);

                if (image == null)
                {
                    return NotFound();
                }

                var model = new DeleteClientImageModel
                {
                    Id = image.Id,
                    ClientId = image.ClientId,
                    ImageUrl = image.ImageUrl,
                    Description = image.Description
                };

                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                var image = await context.ClientImages.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                context.ClientImages.Remove(image);
                await context.SaveChangesAsync();

                return RedirectToAction("Details", "Client", new { id = image.ClientId });
            }
        }
    }

