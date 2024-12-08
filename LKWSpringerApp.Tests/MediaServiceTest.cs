using Moq;
using MockQueryable;
using NUnit.Framework;
using System.Globalization;
using MockQueryable.Moq;

using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.Media;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Http;

namespace LKWSpringerApp.Tests.Services
{
    [TestFixture]
    public class MediaServiceTest
    {
        private Mock<IRepository<Client, Guid>> mockClientRepository;
        private Mock<IRepository<Media, Guid>> mockMediaRepository;
        private MediaService mediaService;

        [SetUp]
        public void Setup()
        {
            mockClientRepository = new Mock<IRepository<Client, Guid>>();
            mockMediaRepository = new Mock<IRepository<Media, Guid>>();
            mediaService = new MediaService(mockMediaRepository.Object, mockClientRepository.Object);
        }

        [Test]
        public async Task IndexGetAllOrderedByClientNameAsync_ShouldReturnPaginatedList_WhenClientsExist()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Augsburg",
                    IsDeleted = false,
                    Media = new List<Media>
                    {
                        new Media { VideoUrl = "https://example.com/video1" },
                        new Media { VideoUrl = null }
                    }
                },
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Ulm",
                    IsDeleted = false,
                    Media = new List<Media>
                    {
                        new Media { VideoUrl = null },
                        new Media { VideoUrl = "https://example.com/video2" }
                    }
                }
            };

            var mockClients = clients.AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            int pageIndex = 1;
            int pageSize = 1;

            var result = await mediaService.IndexGetAllOrderedByClientNameAsync(pageIndex, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(pageSize));
            Assert.That(result.First().ClientName, Is.EqualTo("Augsburg"));
        }

        [Test]
        public async Task GetClientMediaDetailsByIdAsync_ShouldReturnDetails_WhenClientExists()
        {
            var clientId = Guid.NewGuid();
            var client = new Client
            {
                Id = clientId,
                Name = "Test Client",
                IsDeleted = false,
                Media = new List<Media>
                {
                    new Media
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://example.com/image1.jpg",
                        VideoUrl = "https://example.com/video1.mp4",
                        Description = "Description 1"
                    },
                    new Media
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://example.com/image2.jpg",
                        VideoUrl = null,
                        Description = "Description 2"
                    }
                }
            };

            var mockClients = new List<Client> { client }.AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            var result = await mediaService.GetClientMediaDetailsByIdAsync(clientId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ClientId, Is.EqualTo(clientId));
            Assert.That(result.ClientName, Is.EqualTo("Test Client"));
            Assert.That(result.MediaFiles.Count, Is.EqualTo(2));
            Assert.That(result.MediaFiles.First().ImageUrl, Is.EqualTo("https://example.com/image1.jpg"));
            Assert.That(result.MediaFiles.First().VideoUrl, Is.EqualTo("https://example.com/video1.mp4"));
            Assert.That(result.MediaFiles.First().Description, Is.EqualTo("Description 1"));
        }

        [Test]
        public async Task GetClientMediaDetailsByIdAsync_ShouldReturnNull_WhenClientDoesNotExist()
        {
            var clientId = Guid.NewGuid();
            var mockClients = new List<Client>().AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            var result = await mediaService.GetClientMediaDetailsByIdAsync(clientId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllClientsMediaAsync_ShouldReturnAllClientsMedia_WhenClientsExist()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Deutschland",
                    IsDeleted = false,
                    Media = new List<Media>
                    {
                        new Media { VideoUrl = "https://example.com/video1.mp4" },
                        new Media { VideoUrl = null }
                    }
                },
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Kempten",
                    IsDeleted = false,
                    Media = new List<Media>
                    {
                        new Media { VideoUrl = null },
                        new Media { VideoUrl = "https://example.com/video2.mp4" }
                    }
                }
            };

            var mockClients = clients.AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            var result = await mediaService.GetAllClientsMediaAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(clients.Count));
            Assert.That(result.First().ClientName, Is.EqualTo("Deutschland"));
            Assert.That(result.First().MediaCount, Is.EqualTo(3));
            Assert.That(result.Last().ClientName, Is.EqualTo("Kempten"));
            Assert.That(result.Last().MediaCount, Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllClientsMediaAsync_ShouldReturnEmptyList_WhenNoClientsExist()
        {
            var mockClients = new List<Client>().AsQueryable().BuildMock();
            mockClientRepository.Setup(repo => repo.GetAllAttached()).Returns(mockClients);

            var result = await mediaService.GetAllClientsMediaAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetSingleMediaFileByIdAsync_ShouldReturnEditMediaModel_WhenMediaExists()
        {
            var mediaId = Guid.NewGuid();
            var media = new Media
            {
                Id = mediaId,
                ClientId = Guid.NewGuid(),
                ImageUrl = "https://example.com/image.jpg",
                VideoUrl = "https://example.com/video.mp4",
                Description = "Description"
            };

            mockMediaRepository.Setup(repo => repo.GetByIdAsync(mediaId)).ReturnsAsync(media);

            var result = await mediaService.GetSingleMediaFileByIdAsync(mediaId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(mediaId));
            Assert.That(result.ClientId, Is.EqualTo(media.ClientId));
            Assert.That(result.ImageUrl, Is.EqualTo("https://example.com/image.jpg"));
            Assert.That(result.VideoUrl, Is.EqualTo("https://example.com/video.mp4"));
            Assert.That(result.Description, Is.EqualTo("Description"));
        }

        [Test]
        public async Task AddClientMediaAsync_ShouldAddMedia_WhenClientExistsAndFilesAreProvided()
        {
            var clientId = Guid.NewGuid();
            var client = new Client
            {
                Id = clientId,
                Name = "Test Client",
                IsDeleted = false
            };

            var mockImageFile = new Mock<IFormFile>();
            var mockVideoFile = new Mock<IFormFile>();
            var imageFileName = "test-image.jpg";
            var videoFileName = "test-video.mp4";

            mockImageFile.Setup(f => f.FileName).Returns(imageFileName);
            mockVideoFile.Setup(f => f.FileName).Returns(videoFileName);

            var memoryStreamImage = new MemoryStream();
            mockImageFile.Setup(f => f.OpenReadStream()).Returns(memoryStreamImage);
            mockImageFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Callback<Stream, CancellationToken>((stream, token) =>
                {
                    var writer = new StreamWriter(stream);
                    writer.Write("image content");
                    writer.Flush();
                })
                .Returns(Task.CompletedTask);

            var memoryStreamVideo = new MemoryStream();
            mockVideoFile.Setup(f => f.OpenReadStream()).Returns(memoryStreamVideo);
            mockVideoFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Callback<Stream, CancellationToken>((stream, token) =>
                {
                    var writer = new StreamWriter(stream);
                    writer.Write("video content");
                    writer.Flush();
                })
                .Returns(Task.CompletedTask);

            var model = new AddMediaModel
            {
                ClientId = clientId,
                ImageFile = mockImageFile.Object,
                VideoFile = mockVideoFile.Object,
                Description = "Test"
            };

            mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId)).ReturnsAsync(client);

            var mediaList = new List<Media>();
            mockMediaRepository.Setup(repo => repo.AddAsync(It.IsAny<Media>()))
                .Callback<Media>(media => mediaList.Add(media))
                .Returns(Task.CompletedTask);

            await mediaService.AddClientMediaAsync(model);

            Assert.That(mediaList.Count, Is.EqualTo(1));
            var addedMedia = mediaList.First();
            Assert.That(addedMedia.ClientId, Is.EqualTo(clientId));
            Assert.That(addedMedia.ImageUrl, Does.StartWith("media/clients/test_client/"));
            Assert.That(addedMedia.VideoUrl, Does.StartWith("media/clients/test_client/"));
            Assert.That(addedMedia.Description, Is.EqualTo("Test"));
        }

        [Test]
        public void AddClientMediaAsync_ShouldThrowException_WhenClientIsDeleted()
        {
            var clientId = Guid.NewGuid();
            var client = new Client
            {
                Id = clientId,
                Name = "Test Client",
                IsDeleted = true
            };

            var model = new AddMediaModel
            {
                ClientId = clientId,
                ImageFile = null,
                VideoFile = null,
                Description = "Test Description"
            };

            mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId)).ReturnsAsync(client);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await mediaService.AddClientMediaAsync(model));
            Assert.That(ex.Message, Is.EqualTo("Media is deleted or not found."));
        }

        [Test]
        public async Task UpdateClientMediaAsync_ShouldUpdateMedia_WhenValidInputsProvided()
        {
            var mediaId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var media = new Media
            {
                Id = mediaId,
                ClientId = clientId,
                ImageUrl = "media/clients/test_client/old_image.jpg",
                VideoUrl = "media/clients/test_client/old_video.mp4",
                Description = "Old Description"
            };

            var client = new Client
            {
                Id = clientId,
                Name = "Test Client",
                IsDeleted = false
            };

            var mockNewImageFile = new Mock<IFormFile>();
            var newImageFileName = "new_image.jpg";
            mockNewImageFile.Setup(f => f.FileName).Returns(newImageFileName);
            mockNewImageFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var mockNewVideoFile = new Mock<IFormFile>();
            var newVideoFileName = "new_video.mp4";
            mockNewVideoFile.Setup(f => f.FileName).Returns(newVideoFileName);
            mockNewVideoFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var model = new EditMediaModel
            {
                Description = "Updated Description"
            };

            mockMediaRepository.Setup(repo => repo.GetByIdAsync(mediaId)).ReturnsAsync(media);
            mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId)).ReturnsAsync(client);
            mockMediaRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Media>())).ReturnsAsync(true);

            var result = await mediaService.UpdateClientMediaAsync(mediaId, model, mockNewImageFile.Object, mockNewVideoFile.Object);

            Assert.That(result, Is.True);
            Assert.That(media.ImageUrl, Does.StartWith("media/clients/test_client/"));
            Assert.That(media.VideoUrl, Does.StartWith("media/clients/test_client/"));
            Assert.That(media.Description, Is.EqualTo("Updated Description"));
            mockMediaRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Media>()), Times.Once);
        }

        [Test]
        public async Task UpdateClientMediaAsync_ShouldReturnFalse_WhenMediaDoesNotExist()
        {
            var mediaId = Guid.NewGuid();

            mockMediaRepository.Setup(repo => repo.GetByIdAsync(mediaId)).ReturnsAsync((Media)null);

            var model = new EditMediaModel
            {
                Description = "Updated Description"
            };

            var result = await mediaService.UpdateClientMediaAsync(mediaId, model, null, null);

            Assert.That(result, Is.False);
            mockMediaRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Media>()), Times.Never);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnTrue_WhenMediaExists()
        {
            var mediaId = Guid.NewGuid();

            mockMediaRepository.Setup(repo => repo.DeleteAsync(mediaId)).ReturnsAsync(true);

            var result = await mediaService.DeleteAsync(mediaId);

            Assert.That(result, Is.True);
            mockMediaRepository.Verify(repo => repo.DeleteAsync(mediaId), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnFalse_WhenMediaDoesNotExist()
        {
            var mediaId = Guid.NewGuid();

            mockMediaRepository.Setup(repo => repo.DeleteAsync(mediaId)).ReturnsAsync(false);

            var result = await mediaService.DeleteAsync(mediaId);

            Assert.That(result, Is.False);
            mockMediaRepository.Verify(repo => repo.DeleteAsync(mediaId), Times.Once);
        }
    }
}
