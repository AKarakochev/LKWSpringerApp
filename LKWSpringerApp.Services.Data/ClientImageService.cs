using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.ClientImage;
using static LKWSpringerApp.Common.ErrorMessagesConstants.ClientImage;

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
        public async Task<PaginatedList<AllClientImageModel>> IndexGetAllOrderedByClientNameAsync(int pageIndex, int pageSize)
        {
            var query = clientRepository
                .GetAllAttached()
                .Where(c => !c.IsDeleted)
                .Select(c => new AllClientImageModel
                {
                    ClientId = c.Id,
                    ClientName = c.Name,
                    MediaCount = c.Images.Count + c.Images.Where(img => !string.IsNullOrEmpty(img.VideoUrl)).Count()
                })
                .OrderBy(c => c.ClientName);

            return await PaginatedList<AllClientImageModel>.CreateAsync(query, pageIndex, pageSize);
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
        public async Task<List<AllClientImageModel>> GetAllClientsAsync()
        {
            return await clientRepository
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
                throw new ArgumentException(ClientImageIsDeletedOrNotFoundErrorMessage);
            }

            var sanitizedClientName = client.Name.ToLower().Replace(" ", "_");
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media/clients", sanitizedClientName);

            Directory.CreateDirectory(uploadPath);

            string? imageFilePath = null;
            if (model.ImageFile != null)
            {
                var imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                imageFilePath = Path.Combine(uploadPath, imageFileName);
                using (var imageStream = new FileStream(imageFilePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(imageStream);
                }
            }

            string? videoFilePath = null;
            if (model.VideoFile != null)
            {
                var videoFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.VideoFile.FileName)}";
                videoFilePath = Path.Combine(uploadPath, videoFileName);
                using (var videoStream = new FileStream(videoFilePath, FileMode.Create))
                {
                    await model.VideoFile.CopyToAsync(videoStream);
                }
            }

            var clientImage = new ClientImage
            {
                Id = Guid.NewGuid(),
                ClientId = model.ClientId,
                ImageUrl = imageFilePath != null ? $"media/clients/{sanitizedClientName}/{Path.GetFileName(imageFilePath)}" : null,
                VideoUrl = videoFilePath != null ? $"media/clients/{sanitizedClientName}/{Path.GetFileName(videoFilePath)}" : null,
                Description = model.Description
            };

            await clientImageRepository.AddAsync(clientImage);
        }
        public async Task<bool> UpdateClientImageAsync(Guid id, EditClientImageModel model, IFormFile? newImageFile, IFormFile? newVideoFile)
        {
            var image = await clientImageRepository.GetByIdAsync(id);

            if (image == null)
            {
                return false;
            }

            var client = await clientRepository.GetByIdAsync(image.ClientId);
            if (client == null || client.IsDeleted)
            {
                return false;
            }

            var sanitizedClientName = client.Name.ToLower().Replace(" ", "_");
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media/clients", sanitizedClientName);
            Directory.CreateDirectory(uploadPath);

            if (newImageFile != null)
            {
                var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var imageExtension = Path.GetExtension(newImageFile.FileName).ToLower();
                if (!allowedImageExtensions.Contains(imageExtension))
                {
                    throw new ArgumentException(ClientImageInvalidImageFormatErrorMessage);
                }

                var newImageFileName = $"{Guid.NewGuid()}{imageExtension}";
                var newImagePath = Path.Combine(uploadPath, newImageFileName);

                using (var imageStream = new FileStream(newImagePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(imageStream);
                }

                if (!string.IsNullOrEmpty(image.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImageUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                image.ImageUrl = $"media/clients/{sanitizedClientName}/{newImageFileName}";
            }

            if (newVideoFile != null)
            {
                var allowedVideoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv" };
                var videoExtension = Path.GetExtension(newVideoFile.FileName).ToLower();
                if (!allowedVideoExtensions.Contains(videoExtension))
                {
                    throw new ArgumentException(ClientImageInvalidVideoFormatErrorMessage);
                }

                var newVideoFileName = $"{Guid.NewGuid()}{videoExtension}";
                var newVideoPath = Path.Combine(uploadPath, newVideoFileName);

                using (var videoStream = new FileStream(newVideoPath, FileMode.Create))
                {
                    await newVideoFile.CopyToAsync(videoStream);
                }

                if (!string.IsNullOrEmpty(image.VideoUrl))
                {
                    var oldVideoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.VideoUrl);
                    if (File.Exists(oldVideoPath))
                    {
                        File.Delete(oldVideoPath);
                    }
                }

                image.VideoUrl = $"media/clients/{sanitizedClientName}/{newVideoFileName}";
            }

            image.Description = model.Description;

            return await clientImageRepository.UpdateAsync(image);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await clientImageRepository.DeleteAsync(id);
        }
        public async Task<DeleteClientImageModel> GetClientImageByIdAsync(Guid id)
        {
            var image = await clientImageRepository
                .GetAllAttached()
                .Include(ci => ci.Client)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (image == null)
            {
                return null!;
            }

            return new DeleteClientImageModel
            {
                Id = image.Id,
                ClientId = image.ClientId,
                ImageUrl = image.ImageUrl ?? string.Empty,
                Description = image.Description ?? string.Empty
            };
        }
    }
}

