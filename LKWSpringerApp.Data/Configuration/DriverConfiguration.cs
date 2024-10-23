using LKWSpringerApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Data.Configuration
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            //Fluent API
            builder.HasData(this.SeedDrivers());
        }

        private List<Driver> SeedDrivers()
        {
            List<Driver> drivers = new List<Driver>()
            {
                new Driver()
                {
                    FirstName = "Anastas",
                    SecondName = "Karakochev",
                    BirthDate = new DateTime(1985, 11, 07),
                    StartDate = new DateTime(2020, 10, 01),
                    PhoneNumber = "00491624389341",
                    Springerfahrer = true,
                    Stammfahrer = false,
                    IsDeleted = false
                },
                new Driver()
                {
                    FirstName = "Daniel",
                    SecondName = "Schneider",
                    BirthDate = new DateTime(2000, 05, 22),
                    StartDate = new DateTime(2023, 2, 15),
                    PhoneNumber = "00491624494949",
                    Springerfahrer = true,
                    Stammfahrer = false,
                    IsDeleted = false
                },
                new Driver()
                {
                    FirstName = "Max",
                    SecondName = "Mustermann",
                    BirthDate = new DateTime(1970, 12, 24),
                    StartDate = new DateTime(2000, 5, 5),
                    PhoneNumber = "00491624490000",
                    Springerfahrer = false,
                    Stammfahrer = true,
                    IsDeleted = false
                },
                new Driver()
                {
                    FirstName = "Ben",
                    SecondName = "Fischer",
                    BirthDate = new DateTime(1992, 08, 10),
                    StartDate = new DateTime(2022, 12, 11),
                    PhoneNumber = "00491624411111",
                    Springerfahrer = false,
                    Stammfahrer = true,
                    IsDeleted = false
                }
            };

            return drivers;
        }
    }
}
