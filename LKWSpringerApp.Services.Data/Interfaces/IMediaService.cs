using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Web.ViewModels.Media;
using Microsoft.AspNetCore.Http;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IMediaService
    {
        Task<PaginatedList<AllMediaModel>> IndexGetAllOrderedByClientNameAsync(int pageIndex, int pageSize);
        Task<DetailsMediaModel> GetClientMediaDetailsByIdAsync(Guid id);
        Task<List<AllMediaModel>> GetAllClientsMediaAsync();
        Task AddClientMediaAsync(AddMediaModel model);
        Task<EditMediaModel> GetSingleMediaFileByIdAsync(Guid id);
        Task<bool> UpdateClientMediaAsync(Guid id, EditMediaModel model, IFormFile? newImageFile, IFormFile? newVideoFile);
        Task<DeleteMediaModel> GetClientMediaByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
