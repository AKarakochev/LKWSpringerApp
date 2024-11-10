using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Web.Data.Configuration
{
    //public class TourConfiguration : IEntityTypeConfiguration<Tour>
    //{
    //    public void Configure(EntityTypeBuilder<Tour> builder)
    //    {
    //        //Fluent API
    //        builder.HasData(this.SeedTours());
    //    }

    //    private List<Tour> SeedTours()
    //    {
    //        List<Tour> tours = new List<Tour>()
    //        {
    //            new Tour()
    //            {
    //                Id = Guid.NewGuid(),
    //                TourName = "Wangen",
    //                TourNumber = 1,
    //                IsDeleted = false
    //            },
    //            new Tour()
    //            {
    //                Id = Guid.NewGuid(),
    //                TourName = "Kempten",
    //                TourNumber = 2,
    //                IsDeleted = false
    //            },
    //            new Tour()
    //            {
    //                Id = Guid.NewGuid(),
    //                TourName = "Fussen",
    //                TourNumber = 3,
    //                IsDeleted = false
    //            },
    //            new Tour()
    //            {
    //                Id = Guid.NewGuid(),
    //                TourName = "Memmingen",
    //                TourNumber = 4,
    //                IsDeleted = false
    //            }
    //        };

    //        return tours;
    //    }
    //}

}
