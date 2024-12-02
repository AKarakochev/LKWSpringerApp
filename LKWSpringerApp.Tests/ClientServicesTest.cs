using Moq;
using MockQueryable;
using NUnit.Framework;

using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Web.ViewModels.Client;

namespace LKWSpringerApp.Tests.Services
{
    [TestFixture]
    public class ClientServicesTest
    {
        private Mock<IRepository<Client, Guid>> mockClientRepository;
        private ClientService clientService;

        [SetUp]
        public void Setup()
        {
            mockClientRepository = new Mock<IRepository<Client, Guid>>();
            clientService = new ClientService(mockClientRepository.Object);
        }

        [Test]
        public async Task IndexGetAllOrderedByNameAsync_ShouldReturnPaginatedListOrderedByName()
        {
            var clients = new List<Client>
            {
                new Client { Id = Guid.NewGuid(), Name = "Benjamin", ClientNumber = 123, IsDeleted = false },
                new Client { Id = Guid.NewGuid(), Name = "Werner", ClientNumber = 100, IsDeleted = false },
                new Client { Id = Guid.NewGuid(), Name = "Jan", ClientNumber = 999, IsDeleted = false }
            };

            var mockClients = clients.AsQueryable().BuildMock();

            mockClientRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockClients);

            int pageIndex = 1;
            int pageSize = 2;

            var result = await clientService.IndexGetAllOrderedByNameAsync(pageIndex, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Benjamin"));
            Assert.That(result[1].Name, Is.EqualTo("Jan"));
        }

        [Test]
        public async Task IndexGetAllOrderedByNameAsync_ShouldReturnOnlyNonDeletedClients()
        {
            var clients = new List<Client>
            {
                new Client { Id = Guid.NewGuid(), Name = "Benjamin", ClientNumber = 100, IsDeleted = false },
                new Client { Id = Guid.NewGuid(), Name = "Werner", ClientNumber = 444, IsDeleted = true },
                new Client { Id = Guid.NewGuid(), Name = "Jan", ClientNumber = 901, IsDeleted = false }
            };

            var mockClients = clients.AsQueryable().BuildMock();

            mockClientRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockClients);

            int pageIndex = 1;
            int pageSize = 10;

            var result = await clientService.IndexGetAllOrderedByNameAsync(pageIndex, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(c => c.Name == "Werner"), Is.False);
        }

        [Test]
        public async Task IndexGetAllOrderedByNameAsync_ShouldHandleEmptyClientList()
        {
            var clients = new List<Client>();

            var mockClients = clients.AsQueryable().BuildMock();

            mockClientRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockClients);

            int pageIndex = 1;
            int pageSize = 10;

            var result = await clientService.IndexGetAllOrderedByNameAsync(pageIndex, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetClientDetailsByIdAsync_ShouldReturnClientDetails_WhenClientExists()
        {
            var clientId = Guid.NewGuid();
            var clients = new List<Client>
        {
            new Client
            {
                Id = clientId,
                Name = "Benjamin",
                ClientNumber = 10000,
                Address = "87435 Kempten,Salzstr. 1",
                AddressUrl = "https://maps.app.goo.gl/igqAivUWgsAcxgnk7",
                PhoneNumber = "+491624389000",
                DeliveryDescription = "Front door",
                DeliveryTime = "08:00 - 11:00",
                Media = new List<Media>
                {
                    new Media
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://example.com/image1.jpg",
                        VideoUrl = "https://example.com/video1.mp4",
                        Description = "Front view"
                    }
                },
                IsDeleted = false
            }
        };

            var mockClients = clients.AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            var result = await clientService.GetClientDetailsByIdAsync(clientId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(clientId));
            Assert.That(result.Name, Is.EqualTo("Benjamin"));
            Assert.That(result.Images.Count, Is.EqualTo(1));
            Assert.That(result.Images[0].Description, Is.EqualTo("Front view"));
        }

        [Test]
        public async Task GetClientDetailsByIdAsync_ShouldReturnNull_WhenClientDoesNotExist()
        {
            var clientId = Guid.NewGuid();
            var clients = new List<Client>();
            var mockClients = clients.AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            var result = await clientService.GetClientDetailsByIdAsync(clientId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddClientAsync_ShouldAddNewClient_WhenModelIsValid()
        {
            var addClientModel = new AddClientModel
            {
                Name = "Hotel Lipprandt",
                ClientNumber = 9999,
                Address = "Halbinselstraße 62-67, 88142 Wasserburg (Bodensee)",
                AddressUrl = "https://maps.app.goo.gl/EgfNDPk4dUvKykRH6",
                PhoneNumber = "001234567890",
                DeliveryDescription = "Back door",
                DeliveryTime = "07:30 - 10:30"
            };

            await clientService.AddClientAsync(addClientModel);

            mockClientRepository.Verify(repo => repo.AddAsync(It.Is<Client>(c =>
                c.Name == addClientModel.Name &&
                c.ClientNumber == addClientModel.ClientNumber &&
                c.Address == addClientModel.Address &&
                c.AddressUrl == addClientModel.AddressUrl &&
                c.PhoneNumber == addClientModel.PhoneNumber &&
                c.DeliveryDescription == addClientModel.DeliveryDescription &&
                c.DeliveryTime == addClientModel.DeliveryTime &&
                c.IsDeleted == false
            )), Times.Once);
        }

        [Test]
        public async Task UpdateClientAsync_ShouldUpdateClient_WhenModelIsValidAndClientExists()
        {
            var existingClientId = Guid.NewGuid();
            var existingClient = new Client
            {
                Id = existingClientId,
                Name = "Existing Client",
                ClientNumber = 100,
                Address = "Halbinselstraße 62-67, 88142 Wasserburg (Bodensee)",
                AddressUrl = "https://maps.app.goo.gl/EgfNDPk4dUvKykRH6",
                PhoneNumber = "01234567890",
                DeliveryDescription = "You need to use the lift",
                DeliveryTime = "00:00 - 06:30",
                IsDeleted = false
            };

            var editClientModel = new EditClientModel
            {
                Id = existingClientId,
                Name = "Updated Client",
                ClientNumber = 1002,
                Address = "Kemptenerstr. 100,88316 Isny",
                AddressUrl = "https://maps.app.goo.gl/MtYJeuvNfSH7Q7e26",
                PhoneNumber = "+9999999999",
                DeliveryDescription = "Updated delivery description",
                DeliveryTime = "07:00 - 12:00"
            };

            mockClientRepository.Setup(repo => repo.GetByIdAsync(existingClientId)).ReturnsAsync(existingClient);
            mockClientRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Client>())).ReturnsAsync(true);

            var result = await clientService.UpdateClientAsync(editClientModel);

            Assert.That(result, Is.True);
            Assert.That(existingClient.Name, Is.EqualTo(editClientModel.Name));
            Assert.That(existingClient.ClientNumber, Is.EqualTo(editClientModel.ClientNumber));
            Assert.That(existingClient.Address, Is.EqualTo(editClientModel.Address));
            Assert.That(existingClient.AddressUrl, Is.EqualTo(editClientModel.AddressUrl));
            Assert.That(existingClient.PhoneNumber, Is.EqualTo(editClientModel.PhoneNumber));
            Assert.That(existingClient.DeliveryDescription, Is.EqualTo(editClientModel.DeliveryDescription));
            Assert.That(existingClient.DeliveryTime, Is.EqualTo(editClientModel.DeliveryTime));

            mockClientRepository.Verify(repo => repo.UpdateAsync(It.Is<Client>(c => c.Id == existingClientId)), Times.Once);
        }

        [Test]
        public async Task UpdateClientAsync_ShouldReturnFalse_WhenClientDoesNotExist()
        {
            var nonExistentClientId = Guid.NewGuid();
            var editClientModel = new EditClientModel
            {
                Id = nonExistentClientId,
                Name = "Non-existent Client",
                ClientNumber = 1003,
                Address = "1000 Berlin",
                AddressUrl = "https://maps.app.goo.gl/u96nZE8MHFBDuwqu8",
                PhoneNumber = "+1112223333",
                DeliveryDescription = "Non-existent client description",
                DeliveryTime = "05:00 - 08:00"
            };

            mockClientRepository.Setup(repo => repo.GetByIdAsync(nonExistentClientId)).ReturnsAsync((Client)null);

            var result = await clientService.UpdateClientAsync(editClientModel);

            Assert.That(result, Is.False);
            mockClientRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Client>()), Times.Never);
        }

        [Test]
        public async Task SoftDeleteClientAsync_ShouldReturnTrue_WhenClientIsSuccessfullySoftDeleted()
        {
            var clientId = Guid.NewGuid();
            mockClientRepository
                .Setup(repo => repo.SoftDeleteAsync(clientId))
                .ReturnsAsync(true);

            var result = await clientService.SoftDeleteClientAsync(clientId);

            Assert.That(result, Is.True);
            mockClientRepository.Verify(repo => repo.SoftDeleteAsync(clientId), Times.Once);
        }

        [Test]
        public async Task SoftDeleteClientAsync_ShouldReturnFalse_WhenClientDoesNotExist()
        {
            var clientId = Guid.NewGuid();
            mockClientRepository
                .Setup(repo => repo.SoftDeleteAsync(clientId))
                .ReturnsAsync(false);

            var result = await clientService.SoftDeleteClientAsync(clientId);

            Assert.That(result, Is.False);
            mockClientRepository.Verify(repo => repo.SoftDeleteAsync(clientId), Times.Once);
        }
    }
}
