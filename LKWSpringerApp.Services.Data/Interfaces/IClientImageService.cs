using LKWSpringerApp.Web.ViewModels.ClientImage;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IClientImageService
    {
        Task<ICollection<AllClientImageModel>> IndexGetAllOrderedByClientNameAsync();
        Task<DetailsClientImageModel> GetClientImageDetailsByIdAsync(Guid id);
        Task AddClientImageAsync(AddClientImageModel model);
        
        Task<EditClientImageModel> GetSingleMediaFileByIdAsync(Guid id);
        Task<bool> UpdateClientImageAsync(Guid id, EditClientImageModel model, IFormFile? newImageFile);

        Task<DeleteClientImageModel> GetClientImageByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
