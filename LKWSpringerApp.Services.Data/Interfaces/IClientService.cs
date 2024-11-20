using LKWSpringerApp.Web.ViewModels.Client;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    internal interface IClientService
    {
        Task<ICollection<AllClientModel>> IndexGetAllOrederedByNameAsync();

        Task AddClientAsync(AddClientModel model);

        //Dali trqbva pri men da e Guid?
        Task<DetailsClientModel> GetClientDetailsByIdAsync(Guid id);
    }
}
