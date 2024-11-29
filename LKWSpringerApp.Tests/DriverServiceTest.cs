using Moq;
using MockQueryable;
using NUnit.Framework;
using System.Globalization;

using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Web.ViewModels.Driver;
using static LKWSpringerApp.Common.EntityValidationConstants.Driver;
using LKWSpringerApp.Data;


namespace LKWSpringerApp.Tests.Services
{
    [TestFixture]
    public class DriverServiceTests
    {
        private Mock<IRepository<Driver, Guid>> mockDriverRepository;
        private Mock<IRepository<DriverTour, Guid>> mockDriverTourRepository;
        private Mock<LkwSpringerDbContext> mockDbContext;
        private DriverService driverService;

        [SetUp]
        public void Setup()
        {
            mockDriverRepository = new Mock<IRepository<Driver, Guid>>();
            mockDriverTourRepository = new Mock<IRepository<DriverTour, Guid>>();
            mockDbContext = new Mock<LkwSpringerDbContext>();
            driverService = new DriverService(mockDriverRepository.Object, mockDriverTourRepository.Object, mockDbContext.Object);
        }

        [Test]
        public async Task IndexGetAllOrderedBySecondNameAsync_ShouldReturnPaginatedListOrderedBySecondName()
        {
            var drivers = new List<Driver>
            {
                new Driver
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Max",
                    SecondName = "Smith",
                    PhoneNumber = "01624389000",
                    Springerdriver = true,
                    Stammdriver = false,
                    IsDeleted = false
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Denis",
                    SecondName = "Bob",
                    PhoneNumber = "+491624389000",
                    Springerdriver = false,
                    Stammdriver = true,
                    IsDeleted = false
                }
            };

            var mockDrivers = drivers.AsQueryable().BuildMock();

            mockDriverRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockDrivers);

            int pageIndex = 1;
            int pageSize = 10;
           
            var result = await driverService.IndexGetAllOrderedBySecondNameAsync(pageIndex, pageSize);
           
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].SecondName, Is.EqualTo("Bob"));
            Assert.That(result[1].SecondName, Is.EqualTo("Smith"));
        }

        [Test]
        public async Task GetDriverDetailsByIdAsync_ShouldReturnDriverDetails_WhenDriverExists()
        {
            var driverId = Guid.NewGuid();

            var drivers = new List<Driver>
            {
                new Driver
                {
                    Id = driverId,
                    FirstName = "Dan",
                    SecondName = "Doe",
                    BirthDate = new DateTime(1985, 5, 15),
                    StartDate = new DateTime(2020, 1, 1),
                    PhoneNumber = "00491624389000",
                    Springerdriver = true,
                    Stammdriver = false,
                    DriverTours = new List<DriverTour>
                    {
                        new DriverTour
                        {
                            Tour = new Tour
                            {
                                Id = Guid.NewGuid(),
                                TourName = "Wangen",
                                TourNumber = 101
                            }
                        },
                        new DriverTour
                        {
                            Tour = new Tour
                            {
                                Id = Guid.NewGuid(),
                                TourName = "Memmingen",
                                TourNumber = 102
                            }
                        }
                    }
                }
            };

            var mockDrivers = drivers.AsQueryable().BuildMock();

            mockDriverRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockDrivers);

            var result = await driverService.GetDriverDetailsByIdAsync(driverId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(driverId));
            Assert.That(result.FirstName, Is.EqualTo("Dan"));
            Assert.That(result.SecondName, Is.EqualTo("Doe"));
            Assert.That(result.BirthDate, Is.EqualTo("15/05/1985"));
            Assert.That(result.StartDate, Is.EqualTo("01/01/2020"));
            Assert.That(result.Tours.Count, Is.EqualTo(2));
            Assert.That(result.Tours[0].TourName, Is.EqualTo("Wangen"));
        }

        [Test]
        public async Task GetDriverDetailsByIdAsync_ShouldReturnNull_WhenDriverDoesNotExist()
        {
            var driverId = Guid.NewGuid();

            var drivers = new List<Driver>();
            var mockDrivers = drivers.AsQueryable().BuildMock();

            mockDriverRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockDrivers);

            var result = await driverService.GetDriverDetailsByIdAsync(driverId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllDriversAsync_ShouldReturnNonDeletedDrivers()
        {
            var drivers = new List<Driver>
            {
                new Driver
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Max",
                    SecondName = "Smith",
                    IsDeleted = false
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Denis",
                    SecondName = "Bob",
                    IsDeleted = false
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Benjamin",
                    SecondName = "Fischer",
                    IsDeleted = true
                }
            };

            var mockDrivers = drivers.AsQueryable().BuildMock();

            mockDriverRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockDrivers);

            var result = await driverService.GetAllDriversAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(d => d.FirstName == "Max" && d.SecondName == "Smith"), Is.True);
            Assert.That(result.Any(d => d.FirstName == "Denis" && d.SecondName == "Bob"), Is.True);
            Assert.That(result.Any(d => d.FirstName == "Benjamin" && d.SecondName == "Fischer"), Is.False);
        }

        [Test]
        public async Task AddDriverAsync_ShouldAddDriver_WhenValidModelIsProvided()
        {
            var validModel = new AddDriverModel
            {
                FirstName = "Max",
                SecondName = "Smith",
                BirthDate = "15/05/1990", 
                StartDate = "01/01/2022",
                PhoneNumber = "+1234567890",
                Springerdriver = true,
                Stammdriver = false
            };

            await driverService.AddDriverAsync(validModel);

            mockDriverRepository.Verify(repo => repo.AddAsync(It.Is<Driver>(d =>
                d.FirstName == validModel.FirstName &&
                d.SecondName == validModel.SecondName &&
                d.BirthDate == DateTime.ParseExact(validModel.BirthDate, DriverBirthDateFormat, CultureInfo.CurrentCulture) &&
                d.StartDate == DateTime.ParseExact(validModel.StartDate, DriverStartDateFormat, CultureInfo.CurrentCulture) &&
                d.PhoneNumber == validModel.PhoneNumber &&
                d.Springerdriver == validModel.Springerdriver &&
                d.Stammdriver == validModel.Stammdriver
            )), Times.Once);
        }

        [Test]
        public void AddDriverAsync_ShouldThrowException_WhenBirthDateIsInvalid()
        {
            var invalidModel = new AddDriverModel
            {
                FirstName = "Max",
                SecondName = "Smith",
                BirthDate = "InvalidDate",
                StartDate = "01/01/2022",
                PhoneNumber = "+1234567890",
                Springerdriver = true,
                Stammdriver = false
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await driverService.AddDriverAsync(invalidModel));

            Assert.That(ex.ParamName, Is.EqualTo(nameof(invalidModel.BirthDate)));
            Assert.That(ex.Message, Does.Contain("Invalid Birth Date format."));
        }

        [Test]
        public void AddDriverAsync_ShouldThrowException_WhenDriverIsUnderage()
        {
            var underageModel = new AddDriverModel
            {
                FirstName = "Max",
                SecondName = "Smith",
                BirthDate = DateTime.Now.AddYears(-17).ToString("dd/MM/yyyy"),
                StartDate = "01/01/2022",
                PhoneNumber = "+1234567890",
                Springerdriver = true,
                Stammdriver = false
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await driverService.AddDriverAsync(underageModel));

            Assert.That(ex.ParamName, Is.EqualTo(nameof(underageModel.BirthDate)));
            Assert.That(ex.Message, Does.Contain("Driver must be at least 18 years old."));
        }

        [Test]
        public void AddDriverAsync_ShouldThrowException_WhenStartDateIsInvalid()
        {
            var invalidModel = new AddDriverModel
            {
                FirstName = "Max",
                SecondName = "Smith",
                BirthDate = "15/05/1990",
                StartDate = "InvalidDate",
                PhoneNumber = "+1234567890",
                Springerdriver = true,
                Stammdriver = false
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await driverService.AddDriverAsync(invalidModel));

            Assert.That(ex.ParamName, Is.EqualTo(nameof(invalidModel.StartDate)));
            Assert.That(ex.Message, Does.Contain("Invalid Start Date format."));
        }

        [Test]
        public async Task UpdateDriverAsync_ShouldReturnFalse_WhenDriverDoesNotExistOrIsDeleted()
        {
            var editModel = new EditDriverModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Max",
                SecondName = "Smith",
                BirthDate = "15/05/1990",
                StartDate = "01/01/2020",
                PhoneNumber = "+9876543210",
                Springerdriver = true,
                Stammdriver = false,
                SelectedTourIds = new List<Guid> { Guid.NewGuid() }
            };

            mockDriverRepository
                .Setup(repo => repo.GetByIdAsync(editModel.Id))
                .ReturnsAsync((Driver)null);

            var result = await driverService.UpdateDriverAsync(editModel);

            Assert.That(result, Is.False);
            mockDriverRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Driver>()), Times.Never);
        }

        [Test]
        public async Task SoftDeleteDriverAsync_ShouldMarkDriverAsDeleted_WhenDriverExistsAndIsNotDeleted()
        {
            var driverId = Guid.NewGuid();
            var existingDriver = new Driver
            {
                Id = driverId,
                IsDeleted = false
            };

            mockDriverRepository
                .Setup(repo => repo.GetByIdAsync(driverId))
                .ReturnsAsync(existingDriver);

            mockDbContext
                .Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var result = await driverService.SoftDeleteDriverAsync(driverId);

            Assert.That(result, Is.True);
            Assert.That(existingDriver.IsDeleted, Is.True);
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task SoftDeleteDriverAsync_ShouldReturnFalse_WhenDriverDoesNotExistOrIsDeleted()
        {
            var driverId = Guid.NewGuid();

            mockDriverRepository
                .Setup(repo => repo.GetByIdAsync(driverId))
                .ReturnsAsync((Driver)null);

            var result = await driverService.SoftDeleteDriverAsync(driverId);

            Assert.That(result, Is.False);
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}

