using LKWSpringerApp.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Data.Configuration
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasData(this.SeedClientMedia());
        }

        public List<Media> SeedClientMedia()
        {
            List<Media> clientMedia = new List<Media>()
            {
                new Media()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "media/clients/kempten/1.jpg",
                    VideoUrl = "media/clients/kempten/video2.mp4",
                    Description = "Image of Kempten location.",
                    ClientId = Guid.Parse("162ABC8F-AF39-415D-956D-C288A4F401D4")
                },
                new Media()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "media/clients/fussen/1.jpg",
                    VideoUrl = "media/clients/fussen/video1.mp4",
                    Description = "Image of Fussen location.",
                    ClientId = Guid.Parse("0CEAC7E0-F9D5-45F0-9845-8A58141184D5")
                },
                new Media()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "media/clients/wangen/1.jpg",
                    VideoUrl = null,
                    Description = "Image of Wangen location.",
                    ClientId = Guid.Parse("47F3539D-42A7-47C2-86F5-67EBF9638B87")
                },
                new Media()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "media/clients/memmingen/1.jpg",
                    VideoUrl = null,
                    Description = "Image of Memmingen location.",
                    ClientId = Guid.Parse("7A80F16D-F7B0-467C-9F96-61D506702150")
                }
            };

            return clientMedia;
        }
    }
}
