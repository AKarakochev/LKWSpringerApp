using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.ClientImage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Services.Data
{
    public class ClientImageService : IClientImageService
    {
        private readonly IRepository<ClientImage, Guid> clientImageRepository;
        private readonly IRepository<Client, Guid> clientRepository;

        public ClientImageService(IRepository<ClientImage, Guid> clientImageRepository, IRepository<Client, Guid> clientRepository)
        {
            this.clientImageRepository = clientImageRepository;
            this.clientRepository = clientRepository;
        }
        public async Task<ICollection<AllClientImageModel>> IndexGetAllOrderedByClientNameAsync()
        {
            var clients = await clientRepository
                .GetAllAttached()
                .Where(c => !c.IsDeleted)
                .Select(c => new AllClientImageModel
                {
                    ClientId = c.Id,
                    ClientName = c.Name,
                    MediaCount = c.Images.Count + c.Images.Where(img => !string.IsNullOrEmpty(img.VideoUrl)).Count()
                })
                .OrderBy(c => c.ClientName)
                .ToListAsync();

            return clients;
        }
        public async Task<DetailsClientImageModel> GetClientImageDetailsByIdAsync(Guid id)
        {
            var client = await clientRepository
                .GetAllAttached()
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (client == null)
            {
                return null!;
            }

            var model = new DetailsClientImageModel
            {
                ClientId = client.Id,
                ClientName = client.Name,
                MediaFiles = client.Images.Select(img => new MediaFileModel
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    VideoUrl = img.VideoUrl,
                    Description = img.Description
                }).ToList()
            };

            return model;
        }

        public async Task<EditClientImageModel> GetSingleMediaFileByIdAsync(Guid id)
        {
            var image = await clientImageRepository.GetByIdAsync(id);
            if (image == null)
            {
                return null!;
            }

            var model = new EditClientImageModel
            {
                Id = image.Id,
                ClientId = image.ClientId,
                ImageUrl = image.ImageUrl,
                VideoUrl = image.VideoUrl,
                Description = image.Description
            };

            return model;
        }

        public async Task AddClientImageAsync(AddClientImageModel model)
        {
            var client = await clientRepository.GetByIdAsync(model.ClientId);
            if (client == null || client.IsDeleted)
            {
                throw new ArgumentException("Client not found or is deleted.");
            }

            // File path logic (similar to the one in the controller)
            var sanitizedClientName = client.Name.ToLower().Replace(" ", "_");
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clients", sanitizedClientName);

            Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            var clientImage = new ClientImage
            {
                Id = Guid.NewGuid(),
                ClientId = model.ClientId,
                ImageUrl = $"images/clients/{sanitizedClientName}/{fileName}",
                VideoUrl = model.VideoUrl,
                Description = model.Description
            };

            await clientImageRepository.AddAsync(clientImage);
        }

        public async Task<bool> UpdateClientImageAsync(Guid id, EditClientImageModel model, IFormFile? newImageFile)
        {
            var image = await clientImageRepository.GetByIdAsync(model.Id);

            if (image == null)
            {
                return false;
            }

            // Update the image file only if a new file is uploaded
            if (newImageFile != null)
            {
                var client = await clientRepository.GetByIdAsync(image.ClientId);
                if (client == null || client.IsDeleted)
                {
                    return false;
                }

                var sanitizedClientName = client.Name.ToLower().Replace(" ", "_");
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/clients", sanitizedClientName);

                Directory.CreateDirectory(uploadPath);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(newImageFile.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(fileStream);
                }

                // Delete the old file
                if (!string.IsNullOrEmpty(image.ImageUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImageUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                image.ImageUrl = $"images/clients/{sanitizedClientName}/{fileName}";
            }

            // Update other fields
            image.VideoUrl = model.VideoUrl;
            image.Description = model.Description;

            return await clientImageRepository.UpdateAsync(image);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await clientImageRepository.DeleteAsync(id);
        }

        
    }
}

