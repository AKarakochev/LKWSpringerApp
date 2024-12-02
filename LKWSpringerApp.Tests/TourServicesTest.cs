using Moq;
using MockQueryable;
using NUnit.Framework;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Web.ViewModels.TourModels;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;
using LKWSpringerApp.Data;
using LKWSpringerApp.Web.ViewModels.Tour;

namespace LKWSpringerApp.Tests.Services
{
    [TestFixture]
    public class TourServicesTest
    {
        private Mock<IRepository<Tour, Guid>> mockTourRepository;
        private Mock<IRepository<DriverTour, Guid>> mockDriverTourRepository;
        private TourService tourService;
        private Mock<LkwSpringerDbContext> mockDbContext;

        [SetUp]
        public void Setup()
        {
            mockTourRepository = new Mock<IRepository<Tour, Guid>>();
            mockDriverTourRepository = new Mock<IRepository<DriverTour, Guid>>();
            mockDbContext = new Mock<LkwSpringerDbContext>();
            tourService = new TourService(mockTourRepository.Object, mockDriverTourRepository.Object, mockDbContext.Object);
        }

        [Test]
        public async Task IndexGetAllOrderedByTourNameAsync_ShouldReturnEmptyList_WhenNoToursExist()
        {
            var tours = new List<Tour>();
            var mockTours = tours.AsQueryable().BuildMock();

            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            int pageIndex = 1;
            int pageSize = 10;

            var result = await tourService.IndexGetAllOrderedByTourNameAsync(pageIndex, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task IndexGetAllOrderedByTourNameAsync_ShouldExcludeDeletedTours()
        {
            var tours = new List<Tour>
            {
                new Tour { Id = Guid.NewGuid(), TourNumber = 100, TourName = "Munich", IsDeleted = false },
                new Tour { Id = Guid.NewGuid(), TourNumber = 200, TourName = "Berlin", IsDeleted = true },
                new Tour { Id = Guid.NewGuid(), TourNumber = 300, TourName = "Augsburg", IsDeleted = false }
            };

            var mockTours = tours.AsQueryable().BuildMock();

            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            int pageIndex = 1;
            int pageSize = 10;

            var result = await tourService.IndexGetAllOrderedByTourNameAsync(pageIndex, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(t => t.TourName == "Berlin"), Is.False);
        }

        [Test]
        public async Task GetTourDetailsByIdAsync_ShouldReturnTourDetails_WhenTourExists()
        {
            var tourId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var tours = new List<Tour>
        {
            new Tour
            {
                Id = tourId,
                TourNumber = 123,
                TourName = "Test Tour",
                IsDeleted = false,
                ToursClients = new List<TourClient>
                {
                    new TourClient
                    {
                        Client = new Client
                        {
                            Id = clientId,
                            Name = "Test Client",
                            IsDeleted = false
                        }
                    }
                },
                DriverTours = new List<DriverTour>
                {
                    new DriverTour
                    {
                        Driver = new Driver
                        {
                            Id = driverId,
                            FirstName = "Muster",
                            SecondName = "Mustermann"
                        }
                    }
                }
            }
        };

            var mockTours = tours.AsQueryable().BuildMock();
            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            var result = await tourService.GetTourDetailsByIdAsync(tourId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(tourId));
            Assert.That(result.TourName, Is.EqualTo("Test Tour"));
            Assert.That(result.Clients.Count, Is.EqualTo(1));
            Assert.That(result.Clients[0].Name, Is.EqualTo("Test Client"));
            Assert.That(result.Drivers.Count, Is.EqualTo(1));
            Assert.That(result.Drivers[0].FirstName, Is.EqualTo("Muster"));
            Assert.That(result.Drivers[0].SecondName, Is.EqualTo("Mustermann"));
        }

        [Test]
        public async Task GetTourDetailsByIdAsync_ShouldReturnNull_WhenTourDoesNotExist()
        {
            var tourId = Guid.NewGuid();
            var tours = new List<Tour>();
            var mockTours = tours.AsQueryable().BuildMock();
            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            var result = await tourService.GetTourDetailsByIdAsync(tourId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllToursAsync_ShouldReturnAllNonDeletedTours()
        {
            var tours = new List<Tour>
            {
                new Tour { Id = Guid.NewGuid(), TourName = "Tour One", TourNumber = 100, IsDeleted = false },
                new Tour { Id = Guid.NewGuid(), TourName = "Tour Two", TourNumber = 101, IsDeleted = false },
                new Tour { Id = Guid.NewGuid(), TourName = "Tour Three", TourNumber = 102, IsDeleted = true }
            };

            var mockTours = tours.AsQueryable().BuildMock();
            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            var result = await tourService.GetAllToursAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(t => t.TourName == "Tour Three"), Is.False);
            Assert.That(result[0].TourName, Is.EqualTo("Tour One"));
            Assert.That(result[1].TourName, Is.EqualTo("Tour Two"));
        }

        [Test]
        public async Task GetAllToursAsync_ShouldReturnEmptyList_WhenNoToursExist()
        {
            var tours = new List<Tour>();
            var mockTours = tours.AsQueryable().BuildMock();
            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            var result = await tourService.GetAllToursAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task AddTourAsync_ShouldAddNewTourAndDriverTours_WhenTourDoesNotExist()
        {
            var addTourModel = new AddTourModel
            {
                TourName = "New Tour",
                TourNumber = 1234,
                SelectedDriverIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            mockTourRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<Tour>().AsQueryable().BuildMock());

            await tourService.AddTourAsync(addTourModel);

            mockTourRepository.Verify(repo => repo.AddAsync(It.Is<Tour>(t =>
                t.TourName == addTourModel.TourName &&
                t.TourNumber == addTourModel.TourNumber &&
                t.IsDeleted == false
            )), Times.Once);

            foreach (var driverId in addTourModel.SelectedDriverIds)
            {
                mockDriverTourRepository.Verify(repo => repo.AddAsync(It.Is<DriverTour>(dt =>
                    dt.DriverId == driverId &&
                    dt.TourId != Guid.Empty
                )), Times.Once);
            }

            mockTourRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void AddTourAsync_ShouldThrowArgumentException_WhenTourWithSameNameOrNumberExists()
        {
            var existingTour = new Tour
            {
                Id = Guid.NewGuid(),
                TourName = "Existing Tour",
                TourNumber = 1234,
                IsDeleted = false
            };

            var addTourModel = new AddTourModel
            {
                TourName = "Existing Tour",
                TourNumber = 1234,
                SelectedDriverIds = new List<Guid> { Guid.NewGuid() }
            };

            var mockTours = new List<Tour> { existingTour }.AsQueryable().BuildMock();
            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => tourService.AddTourAsync(addTourModel));
            Assert.That(ex.Message, Is.EqualTo(TourWithSameNumberErrorMessage));

            mockTourRepository.Verify(repo => repo.AddAsync(It.IsAny<Tour>()), Times.Never);
            mockDriverTourRepository.Verify(repo => repo.AddAsync(It.IsAny<DriverTour>()), Times.Never);
            mockTourRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task UpdateTourAsync_ShouldReturnFalse_WhenTourDoesNotExist()
        {
            var editTourModel = new EditTourModel
            {
                Id = Guid.NewGuid(),
                TourName = "Updated Tour",
                TourNumber = 1000,
                SelectedDriverIds = new List<Guid> { Guid.NewGuid() }
            };

            mockTourRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<Tour>().AsQueryable().BuildMock());

            var result = await tourService.UpdateTourAsync(editTourModel);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateTourAsync_ShouldUpdateTourDetailsAndAddDrivers()
        {
            var tourId = Guid.NewGuid();
            var existingDriverId = Guid.NewGuid();
            var newDriverId = Guid.NewGuid();

            var tour = new Tour
            {
                Id = tourId,
                TourName = "Old Tour Name",
                TourNumber = 555,
                DriverTours = new List<DriverTour>
                {
                    new DriverTour { DriverId = existingDriverId, TourId = tourId }
                },
                IsDeleted = false
            };

            var editTourModel = new EditTourModel
            {
                Id = tourId,
                TourName = "Updated Tour Name",
                TourNumber = 7777,
                SelectedDriverIds = new List<Guid> { existingDriverId, newDriverId }
            };

            var mockTours = new List<Tour> { tour }.AsQueryable().BuildMock();

            mockTourRepository.Setup(repo => repo.GetAllAttached()).Returns(mockTours);

            mockDriverTourRepository
                .Setup(repo => repo.AddAsync(It.IsAny<DriverTour>()))
                .Returns(Task.CompletedTask);

            mockDriverTourRepository
                .Setup(repo => repo.Delete(It.IsAny<DriverTour>()));

            mockDbContext
                .Setup(db => db.SaveChangesAsync(default))
                .ReturnsAsync(1);

            var result = await tourService.UpdateTourAsync(editTourModel);

            Assert.That(result, Is.True);
            Assert.That(tour.TourName, Is.EqualTo(editTourModel.TourName));
            Assert.That(tour.TourNumber, Is.EqualTo(editTourModel.TourNumber));

            mockDriverTourRepository.Verify(repo => repo.AddAsync(It.Is<DriverTour>(dt =>
                dt.DriverId == newDriverId &&
                dt.TourId == tourId
            )), Times.Once);

            mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task SoftDeleteTourAsync_ShouldReturnTrue_WhenTourIsSuccessfullySoftDeleted()
        {
            var tourId = Guid.NewGuid();

            var tour = new Tour
            {
                Id = tourId,
                IsDeleted = false
            };

            mockTourRepository
                .Setup(repo => repo.GetByIdAsync(tourId))
                .ReturnsAsync(tour);

            mockDbContext
                .Setup(db => db.SaveChangesAsync(default))
                .ReturnsAsync(1);

            var result = await tourService.SoftDeleteTourAsync(tourId);

            Assert.That(result, Is.True);
            Assert.That(tour.IsDeleted, Is.True);

            mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task SoftDeleteTourAsync_ShouldReturnFalse_WhenTourDoesNotExist()
        {
            var nonExistentTourId = Guid.NewGuid();

            mockTourRepository
                .Setup(repo => repo.GetByIdAsync(nonExistentTourId))
                .ReturnsAsync((Tour)null);

            var result = await tourService.SoftDeleteTourAsync(nonExistentTourId);

            Assert.That(result, Is.False);
            mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Never);
        }

    }
}
