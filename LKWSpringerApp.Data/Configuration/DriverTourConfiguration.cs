using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Data.Configuration
{
    public class DriverTourConfiguration : IEntityTypeConfiguration<DriverTour>
    {
        public void Configure(EntityTypeBuilder<DriverTour> builder)
        {
            //Fluent API
            builder.HasData(this.SeedDriverTour());
        }

        private List<DriverTour> SeedDriverTour() // Return List<DriverTour> here
        {
            List<DriverTour> driverTours = new List<DriverTour>
            {
                new DriverTour
                {
                    DriverId = new Guid("8654035C-E140-4FC7-B9DD-1A36E2A09186"),
                    TourId = new Guid("1F500845-25EF-4A18-9FDC-14F69568CF1F")
                },
                new DriverTour
                {
                    DriverId = new Guid("86770804-CD07-4471-ACCA-84E83AD0026B"),
                    TourId = new Guid("7B520787-18DF-44D4-8BE2-292411CBCB68")
                },
                new DriverTour
                {
                    DriverId = new Guid("7959723F-22E9-4EFB-A334-CF25C5BD9431"),
                    TourId = new Guid("CEF8EEB6-D07C-42CE-959F-CAE8C1FAE542")
                },
                new DriverTour
                {
                    DriverId = new Guid("A22DD3BC-24F9-4DEA-B986-8A198D460A8F"),
                    TourId = new Guid("A3101694-8D27-4D93-8B76-A2BC7CDEED7A")
                }
            };

            return driverTours;
        }
    }
}
