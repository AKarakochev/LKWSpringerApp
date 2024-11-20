using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Client;

namespace LKWSpringerApp.Services.Data
{
    public class ClientService : IClientService
    {
        private IRepository<Client, Guid> clientRepository;

        public ClientService(IRepository<Client, Guid> clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        public Task AddClientAsync(AddClientModel model)
        {
            throw new NotImplementedException();
        }

        public Task<DetailsClientModel> GetClientDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<AllClientModel>> IndexGetAllOrederedByNameAsync()
        {
            throw new NotImplementedException();
        }
    }
}
