using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Client;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Services.Data
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client, Guid> clientRepository;

        public ClientService(IRepository<Client, Guid> clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        public async Task AddClientAsync(AddClientModel model)
        {
            var newClient = new Client
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ClientNumber = model.ClientNumber ?? 0,
                Address = model.Address,
                AddressUrl = model.AddressUrl,
                PhoneNumber = model.PhoneNumber,
                DeliveryDescription = model.DeliveryDescription,
                DeliveryTime = model.DeliveryTime,
                IsDeleted = false
            };

            await clientRepository.AddAsync(newClient);
        }

        public async Task<DetailsClientModel> GetClientDetailsByIdAsync(Guid id)
        {
            var client = await this.clientRepository
                .GetAllAttached()
                .Where(c => c.Id == id && !c.IsDeleted)
                .Select(c => new DetailsClientModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ClientNumber = c.ClientNumber,
                        Address = c.Address,
                        AddressUrl = c.AddressUrl,
                        PhoneNumber = c.PhoneNumber,
                        DeliveryDescription = c.DeliveryDescription,
                        DeliveryTime = c.DeliveryTime,
                        Images = c.Images.Select(img => new ClientImageModel
                                {
                                    Id = img.Id,
                                    ImageUrl = img.ImageUrl,
                                    VideoUrl = img.VideoUrl,
                                    Description = img.Description
                                }).ToList()
                    })
                    .FirstOrDefaultAsync();

            return client;
        }

        public async Task<bool> UpdateClientAsync(EditClientModel model)
        {
            var client = await clientRepository.GetByIdAsync(model.Id);

            if (client == null || client.IsDeleted)
            {
                return false;
            }

            client.Name = model.Name;
            client.ClientNumber = model.ClientNumber;
            client.Address = model.Address;
            client.AddressUrl = model.AddressUrl;
            client.PhoneNumber = model.PhoneNumber;
            client.DeliveryDescription = model.DeliveryDescription;
            client.DeliveryTime = model.DeliveryTime;

            return await clientRepository.UpdateAsync(client);
        }

        public async Task<bool> SoftDeleteClientAsync(Guid id)
        {
            return await clientRepository.SoftDeleteAsync(id);
        }

        public async Task<ICollection<AllClientModel>> IndexGetAllOrderedByNameAsync()
        {
            var clients = await this.clientRepository
                .GetAllAttached()
                .Where(c => !c.IsDeleted)
                .Select(c => new AllClientModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClientNumber = c.ClientNumber,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    DeliveryDescription = c.DeliveryDescription,
                    DeliveryTime = c.DeliveryTime,
                    AddressUrl = c.AddressUrl,
                    Images = c.Images.Select(img => new ClientImageModel
                    {
                        Id = img.Id,
                        ImageUrl = img.ImageUrl,
                        VideoUrl = img.VideoUrl,
                        Description = img.Description
                    }).ToList()
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return clients;
        }
    }
}
