using LKWSpringerApp.Web.ViewModels.ClientImage;
using LKWSpringerApp.Services.Data.Helpers;

using Microsoft.AspNetCore.Http;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IClientImageService
    {
        Task<PaginatedList<AllClientImageModel>> IndexGetAllOrderedByClientNameAsync(int pageIndex, int pageSize);
        Task<DetailsClientImageModel> GetClientImageDetailsByIdAsync(Guid id);
        Task<List<AllClientImageModel>> GetAllClientsAsync();
        Task AddClientImageAsync(AddClientImageModel model);
        
        Task<EditClientImageModel> GetSingleMediaFileByIdAsync(Guid id);
        Task<bool> UpdateClientImageAsync(Guid id, EditClientImageModel model, IFormFile? newImageFile);

        Task<DeleteClientImageModel> GetClientImageByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
