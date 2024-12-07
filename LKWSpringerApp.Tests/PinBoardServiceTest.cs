using Moq;
using NUnit.Framework;
using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Web.ViewModels.PinBoard;
using MockQueryable.Moq;

namespace LKWSpringerApp.Tests.Services
{
    [TestFixture]
    public class PinBoardServiceTest
    {
        private Mock<IRepository<PinBoard, Guid>> mockPinBoardRepository;
        private Mock<LkwSpringerDbContext> mockDbContext;
        private PinBoardService pinBoardService;

        [SetUp]
        public void Setup()
        {
            mockPinBoardRepository = new Mock<IRepository<PinBoard, Guid>>();
            mockDbContext = new Mock<LkwSpringerDbContext>();
            pinBoardService = new PinBoardService(mockDbContext.Object);
        }

        [Test]
        public async Task GetNewsAsync_ShouldReturnNews_WhenPinBoardExists()
        {
            var pinBoards = new List<PinBoard>
            {
                new PinBoard
                {
                    News = "Driver meeting at office.",
                    ImportantNews = "Expecting snow.Be careful on the road!"
                }
            };

            var mockPinBoards = pinBoards.AsQueryable().BuildMockDbSet();

            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);

            var result = await pinBoardService.GetNewsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.News, Is.EqualTo("Driver meeting at office."));
            Assert.That(result.ImportantNews, Is.EqualTo("Expecting snow.Be careful on the road!"));
        }

        [Test]
        public async Task GetNewsAsync_ShouldReturnDefaultModel_WhenNoPinBoardExists()
        {
            var pinBoards = new List<PinBoard>();

            var mockPinBoards = pinBoards.AsQueryable().BuildMockDbSet();

            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);

            var result = await pinBoardService.GetNewsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.News, Is.EqualTo("No news available"));
            Assert.That(result.ImportantNews, Is.EqualTo("No important news available"));
        }

        [Test]
        public async Task EditNewsAsync_ShouldUpdateNews_WhenPinBoardExists()
        {
            var existingPinBoard = new PinBoard
            {
                News = "Old News",
                ImportantNews = "Old Important News"
            };

            var mockPinBoards = new List<PinBoard> { existingPinBoard }
                .AsQueryable()
                .BuildMockDbSet();

            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);
            mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var newModel = new PinBoardNewsModel
            {
                News = "Updated News",
                ImportantNews = "Updated Important News"
            };

            await pinBoardService.EditNewsAsync(newModel);

            Assert.That(existingPinBoard.News, Is.EqualTo("Updated News"));
            Assert.That(existingPinBoard.ImportantNews, Is.EqualTo("Updated Important News"));
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetPinBoardDataForDriverAsync_ShouldReturnDetails_WhenPinBoardExists()
        {
            var driverId = Guid.NewGuid();
            var existingPinBoard = new PinBoard
            {
                DriverId = driverId,
                DrivingLicenseExpDate = new DateTime(2025, 5, 1),
                DrivingCardExpDate = new DateTime(2025, 6, 1),
                DrivingLicenseRenewalDate = new DateTime(2023, 11, 1),
                DrivingCardRenewalDate = new DateTime(2024, 12, 1),
                UpcomingCourse = "Safety Training",
                UpcomingCourseDate = new DateTime(2024, 1, 15),
                News = "Driver of the Month",
                ImportantNews = "Health Check"
            };

            var mockPinBoards = new List<PinBoard> { existingPinBoard }
                .AsQueryable()
                .BuildMockDbSet();

            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);

            var result = await pinBoardService.GetPinBoardDataForDriverAsync(driverId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.DriverId, Is.EqualTo(driverId));
            Assert.That(result.DrivingLicenseExpDate, Is.EqualTo("05/2025"));
            Assert.That(result.DrivingCardExpDate, Is.EqualTo("06/2025"));
            Assert.That(result.DrivingLicenseRenewalDate, Is.EqualTo("11/2023"));
            Assert.That(result.DrivingCardRenewalDate, Is.EqualTo("12/2024"));
            Assert.That(result.UpcomingCourse, Is.EqualTo("Safety Training"));
            Assert.That(result.UpcomingCourseDate, Is.EqualTo("15/01/2024"));
            Assert.That(result.News, Is.EqualTo("Driver of the Month"));
            Assert.That(result.ImportantNews, Is.EqualTo("Health Check"));
        }

        [Test]
        public async Task GetPinBoardDataForDriverAsync_ShouldCreateAndReturnNewPinBoard_WhenPinBoardDoesNotExist()
        {
            var driverId = Guid.NewGuid();

            var mockPinBoards = new List<PinBoard>().AsQueryable().BuildMockDbSet();

            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);
            mockDbContext.Setup(db => db.PinBoards.AddAsync(It.IsAny<PinBoard>(), It.IsAny<CancellationToken>()))
                         .Callback<PinBoard, CancellationToken>((pb, _) => mockPinBoards.Object.Add(pb));

            mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await pinBoardService.GetPinBoardDataForDriverAsync(driverId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.DriverId, Is.EqualTo(driverId));
            Assert.That(result.DrivingLicenseExpDate, Is.EqualTo(DateTime.Now.ToString("MM/yyyy")));
            Assert.That(result.DrivingCardExpDate, Is.EqualTo(DateTime.Now.ToString("MM/yyyy")));
            Assert.That(result.DrivingLicenseRenewalDate, Is.Null);
            Assert.That(result.DrivingCardRenewalDate, Is.Null);
            Assert.That(result.UpcomingCourse, Is.Null);
            Assert.That(result.UpcomingCourseDate, Is.Null);
            Assert.That(result.News, Is.EqualTo("No news available"));
            Assert.That(result.ImportantNews, Is.EqualTo("No important news available"));

            mockDbContext.Verify(db => db.PinBoards.AddAsync(It.IsAny<PinBoard>(), It.IsAny<CancellationToken>()), Times.Once);
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetPinBoardDataForEditAsync_ShouldReturnEditModel_WhenPinBoardExists()
        {
            var driverId = Guid.NewGuid();
            var existingPinBoard = new PinBoard
            {
                DriverId = driverId,
                DrivingLicenseExpDate = new DateTime(2025, 5, 1),
                DrivingCardExpDate = new DateTime(2025, 6, 1),
                DrivingLicenseRenewalDate = new DateTime(2023, 11, 1),
                DrivingCardRenewalDate = new DateTime(2024, 12, 1),
                UpcomingCourse = "Safety",
                UpcomingCourseDate = new DateTime(2024, 1, 15)
            };

            var mockPinBoards = new List<PinBoard> { existingPinBoard }.AsQueryable().BuildMockDbSet();
            var mockDrivers = new List<Driver> { new Driver { Id = driverId, IsDeleted = false } }.AsQueryable().BuildMockDbSet();

            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);
            mockDbContext.Setup(db => db.Drivers).Returns(mockDrivers.Object);

            var result = await pinBoardService.GetPinBoardDataForEditAsync(driverId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.DriverId, Is.EqualTo(driverId));
            Assert.That(result.DrivingLicenseExpDate, Is.EqualTo("05/2025"));
            Assert.That(result.DrivingCardExpDate, Is.EqualTo("06/2025"));
            Assert.That(result.DrivingLicenseRenewalDate, Is.EqualTo("11/2023"));
            Assert.That(result.DrivingCardRenewalDate, Is.EqualTo("12/2024"));
            Assert.That(result.UpcomingCourse, Is.EqualTo("Safety"));
            Assert.That(result.UpcomingCourseDate, Is.EqualTo("15/01/2024"));
        }

        [Test]
        public async Task GetPinBoardDataForEditAsync_ShouldThrowException_WhenDriverDoesNotExist()
        {
            var driverId = Guid.NewGuid();

            var mockDrivers = new List<Driver>().AsQueryable().BuildMockDbSet();
            mockDbContext.Setup(db => db.Drivers).Returns(mockDrivers.Object);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await pinBoardService.GetPinBoardDataForEditAsync(driverId));

            Assert.That(ex.Message, Is.EqualTo("Driver does not exist."));
        }

        [Test]
        public async Task UpdatePinBoardAsync_ShouldUpdatePinBoard_WhenValidModelIsProvided()
        {
            var driverId = Guid.NewGuid();
            var existingPinBoard = new PinBoard
            {
                DriverId = driverId,
                DrivingLicenseExpDate = new DateTime(2025, 5, 1),
                DrivingCardExpDate = new DateTime(2025, 6, 1)
            };

            var mockPinBoards = new List<PinBoard> { existingPinBoard }.AsQueryable().BuildMockDbSet();
            mockDbContext.Setup(db => db.PinBoards).Returns(mockPinBoards.Object);
            mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var editModel = new PinBoardEditDriverModel
            {
                DriverId = driverId,
                DrivingLicenseExpDate = "07/2025",
                DrivingCardExpDate = "08/2025",
                DrivingLicenseRenewalDate = "09/2023",
                DrivingCardRenewalDate = "10/2023",
                UpcomingCourse = "Training",
                UpcomingCourseDate = "20/02/2024"
            };

            await pinBoardService.UpdatePinBoardAsync(editModel);

            Assert.That(existingPinBoard.DrivingLicenseExpDate, Is.EqualTo(new DateTime(2025, 7, 1)));
            Assert.That(existingPinBoard.DrivingCardExpDate, Is.EqualTo(new DateTime(2025, 8, 1)));
            Assert.That(existingPinBoard.DrivingLicenseRenewalDate, Is.EqualTo(new DateTime(2023, 9, 1)));
            Assert.That(existingPinBoard.DrivingCardRenewalDate, Is.EqualTo(new DateTime(2023, 10, 1)));
            Assert.That(existingPinBoard.UpcomingCourse, Is.EqualTo("Training"));
            Assert.That(existingPinBoard.UpcomingCourseDate, Is.EqualTo(new DateTime(2024, 2, 20)));
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

    