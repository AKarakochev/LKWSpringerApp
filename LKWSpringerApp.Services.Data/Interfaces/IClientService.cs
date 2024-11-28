using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Web.ViewModels.Client;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IClientService
    {
        Task<PaginatedList<AllClientModel>> IndexGetAllOrderedByNameAsync(int pageIndex, int pageSize);
        Task AddClientAsync(AddClientModel model);
        Task<DetailsClientModel> GetClientDetailsByIdAsync(Guid id);
        Task<bool> UpdateClientAsync(EditClientModel model);
        Task<bool> SoftDeleteClientAsync(Guid id);
    }
}
