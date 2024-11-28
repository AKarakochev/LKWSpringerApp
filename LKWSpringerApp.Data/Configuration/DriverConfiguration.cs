using LKWSpringerApp.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Data.Configuration
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasData(this.SeedDrivers());
        }

        private List<Driver> SeedDrivers()
        {
            List<Driver> drivers = new List<Driver>()
            {
                new Driver
            {
                Id = new Guid("8654035C-E140-4FC7-B9DD-1A36E2A09186"),
                FirstName = "Anastas",
                SecondName = "Karakochev",
                BirthDate = new DateTime(1985, 11, 07),
                StartDate = new DateTime(2020, 10, 01),
                PhoneNumber = "00491624389341",
                Springerdriver = true,
                Stammdriver = false,
                IsDeleted = false
            },
            new Driver
            {
                Id = new Guid("86770804-CD07-4471-ACCA-84E83AD0026B"),
                FirstName = "Daniel",
                SecondName = "Schneider",
                BirthDate = new DateTime(2000, 05, 22),
                StartDate = new DateTime(2023, 2, 15),
                PhoneNumber = "00491624494949",
                Springerdriver = true,
                Stammdriver = false,
                IsDeleted = false
            },
            new Driver
            {
                Id = new Guid("7959723F-22E9-4EFB-A334-CF25C5BD9431"),
                FirstName = "Max",
                SecondName = "Mustermann",
                BirthDate = new DateTime(1970, 12, 24),
                StartDate = new DateTime(2000, 5, 5),
                PhoneNumber = "00491624490000",
                Springerdriver = false,
                Stammdriver = true,
                IsDeleted = false
            },
            new Driver
            {
                Id = new Guid("A22DD3BC-24F9-4DEA-B986-8A198D460A8F"),
                FirstName = "Ben",
                SecondName = "Fischer",
                BirthDate = new DateTime(1992, 08, 10),
                StartDate = new DateTime(2022, 12, 11),
                PhoneNumber = "00491624411111",
                Springerdriver = false,
                Stammdriver = true,
                IsDeleted = false
                }
            };

            return drivers;
        }
    }
}
