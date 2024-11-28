using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Web.Data.Configuration
{
    public class ClientImageConfiguration : IEntityTypeConfiguration<ClientImage>
    {
        public void Configure(EntityTypeBuilder<ClientImage> builder)
        {
            builder.HasData(this.SeedClientImages());
        }

        public List<ClientImage> SeedClientImages()
        {
            List<ClientImage> clientImages = new List<ClientImage>()
            {
                new ClientImage()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "images/clients/kempten/1.jpg",
                    VideoUrl = null,
                    Description = "Image of Kempten location.",
                    ClientId = Guid.Parse("162ABC8F-AF39-415D-956D-C288A4F401D4")
                },
                new ClientImage()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "images/clients/fussen/1.jpg",
                    VideoUrl = null,
                    Description = "Image of Fussen location.",
                    ClientId = Guid.Parse("0CEAC7E0-F9D5-45F0-9845-8A58141184D5")
                },
                new ClientImage()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "images/clients/wangen/1.jpg",
                    VideoUrl = null,
                    Description = "Image of Wangen location.",
                    ClientId = Guid.Parse("47F3539D-42A7-47C2-86F5-67EBF9638B87")
                },
                new ClientImage()
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "images/clients/memmingen/1.jpg",
                    VideoUrl = null,
                    Description = "Image of Memmingen location.",
                    ClientId = Guid.Parse("7A80F16D-F7B0-467C-9F96-61D506702150")
                }
            };

            return clientImages;
        }
    }
}
