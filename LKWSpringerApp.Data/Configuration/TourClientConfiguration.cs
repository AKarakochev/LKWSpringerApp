using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Data.Configuration
{
    public class TourClientConfiguration : IEntityTypeConfiguration<TourClient>
    {
        public void Configure(EntityTypeBuilder<TourClient> builder)
        {
            builder.HasData(new List<TourClient>
        {
            new TourClient
            {
                TourId = new Guid("1F500845-25EF-4A18-9FDC-14F69568CF1F"),
                ClientId = new Guid("162ABC8F-AF39-415D-956D-C288A4F401D4")
            },
            new TourClient
            {
                TourId = new Guid("7B520787-18DF-44D4-8BE2-292411CBCB68"),
                ClientId = new Guid("0CEAC7E0-F9D5-45F0-9845-8A58141184D5")
            },
            new TourClient
            {
                TourId = new Guid("CEF8EEB6-D07C-42CE-959F-CAE8C1FAE542"),
                ClientId = new Guid("47F3539D-42A7-47C2-86F5-67EBF9638B87")
            },
            new TourClient
            {
                TourId = new Guid("A3101694-8D27-4D93-8B76-A2BC7CDEED7A"),
                ClientId = new Guid("7A80F16D-F7B0-467C-9F96-61D506702150")
            },

        });
        }
    }
}
