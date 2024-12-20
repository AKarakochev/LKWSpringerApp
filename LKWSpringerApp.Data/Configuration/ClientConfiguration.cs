﻿using LKWSpringerApp.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LKWSpringerApp.Web.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(this.SeedClients());
        }

        private List<Client> SeedClients()
        {
            List<Client> clients = new List<Client>()
            {
                new Client()
                {
                    Id = new Guid("162ABC8F-AF39-415D-956D-C288A4F401D4"),
                    Name = "Kempten",
                    ClientNumber = 101,
                    Address = "87435 Kempten,Kemptener Str. 1",
                    AddressUrl = "https://maps.app.goo.gl/DZyJSoceAaAvx1cN9",
                    PhoneNumber = "+491624389000",
                    DeliveryDescription = "Front door",
                    DeliveryTime = "04:00",
                    IsDeleted = false
                },
                new Client()
                {
                    Id = new Guid("0CEAC7E0-F9D5-45F0-9845-8A58141184D5"),
                    Name = "Fussen",
                    ClientNumber = 3000,
                    Address = "87629 Fussen,Fussenner Str. 2",
                    AddressUrl = "https://maps.app.goo.gl/GzDSJXPr1PFcpXmb9",
                    PhoneNumber = "+491624389111",
                    DeliveryDescription = "Hospital main entrance",
                    DeliveryTime = "06:00",
                    IsDeleted = false
                },
                new Client()
                {
                    Id = new Guid("47F3539D-42A7-47C2-86F5-67EBF9638B87"),
                    Name = "Wangen",
                    ClientNumber = 5555,
                    Address = "87000 Wangen,Wangener Str. 3",
                    AddressUrl = "https://maps.app.goo.gl/NHu42wMmYcDWMJop6",
                    PhoneNumber = "+491624389222",
                    DeliveryDescription = "Ramp 13",
                    DeliveryTime = "02:30",
                    IsDeleted = false
                },
                new Client()
                {
                    Id = new Guid("7A80F16D-F7B0-467C-9F96-61D506702150"),
                    Name = "Memmingen",
                    ClientNumber = 110,
                    Address = "87435 Memmingen,Memmingener Str. 4",
                    AddressUrl = "https://maps.app.goo.gl/ZzMGLmmM15hgpEFY9",
                    PhoneNumber = "+491624389333",
                    DeliveryDescription = "Behind the restaurant",
                    DeliveryTime = "10:00",
                    IsDeleted = false
                }
            };

            return clients;
        }
    }
}
