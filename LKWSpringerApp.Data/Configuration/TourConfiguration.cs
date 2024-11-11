using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Web.Data.Configuration
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            //Fluent API
            builder.HasData(this.SeedTours());
        }

        private List<Tour> SeedTours()
        {
            List<Tour> tours = new List<Tour>()
            {
                new Tour
            {
                Id = new Guid("1F500845-25EF-4A18-9FDC-14F69568CF1F"),
                TourName = "Wangen",
                TourNumber = 1,
                IsDeleted = false
            },
            new Tour
            {
                Id = new Guid("7B520787-18DF-44D4-8BE2-292411CBCB68"),
                TourName = "Kempten",
                TourNumber = 2,
                IsDeleted = false
            },
            new Tour
            {
                Id = new Guid("CEF8EEB6-D07C-42CE-959F-CAE8C1FAE542"),
                TourName = "Fussen",
                TourNumber = 3,
                IsDeleted = false
            },
            new Tour
            {
                Id = new Guid("A3101694-8D27-4D93-8B76-A2BC7CDEED7A"),
                TourName = "Memmingen",
                TourNumber = 4,
                IsDeleted = false
                }
            };

            return tours;
        }
    }

}
