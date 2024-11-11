using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTourClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87629 Fussen,Fussenner Str. 2", 3000, "Hospital main entrance", "06:00", "Fussen", "+491624389111" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("162abc8f-af39-415d-956d-c288a4f401d4"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87435 Kempten,Kemptener Str. 1", 101, "Front door", "04:00", "Kempten", "+491624389000" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87000 Wangen,Wangener Str. 3", 5555, "Ramp 13", "02:30", "Wangen", "+491624389222" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87435 Memmingen,Memmingener Str. 4", 110, "Behind the restaurant", "10:00", "Memmingen", "+491624389333" });

            migrationBuilder.InsertData(
                table: "TourClients",
                columns: new[] { "ClientId", "TourId" },
                values: new object[,]
                {
                    { new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f") },
                    { new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), new Guid("7b520787-18df-44d4-8be2-292411cbcb68") },
                    { new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a") },
                    { new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542") }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"), "Wangen", 1 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("7b520787-18df-44d4-8be2-292411cbcb68"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("86770804-cd07-4471-acca-84e83ad0026b"), "Kempten", 2 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"), "Memmingen", 4 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"), "Fussen", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TourClients",
                keyColumns: new[] { "ClientId", "TourId" },
                keyValues: new object[] { new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f") });

            migrationBuilder.DeleteData(
                table: "TourClients",
                keyColumns: new[] { "ClientId", "TourId" },
                keyValues: new object[] { new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), new Guid("7b520787-18df-44d4-8be2-292411cbcb68") });

            migrationBuilder.DeleteData(
                table: "TourClients",
                keyColumns: new[] { "ClientId", "TourId" },
                keyValues: new object[] { new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a") });

            migrationBuilder.DeleteData(
                table: "TourClients",
                keyColumns: new[] { "ClientId", "TourId" },
                keyValues: new object[] { new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542") });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87435 Memmingen,Memmingener Str. 4", 110, "Behind the restaurant", "10:00", "Memmingen", "+491624389333" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("162abc8f-af39-415d-956d-c288a4f401d4"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87629 Fussen,Fussenner Str. 2", 3000, "Hospital main entrance", "06:00", "Fussen", "+491624389111" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87435 Kempten,Kemptener Str. 1", 101, "Front door", "04:00", "Kempten", "+491624389000" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"),
                columns: new[] { "Address", "ClientNumber", "DeliveryDescription", "DeliveryTime", "Name", "PhoneNumber" },
                values: new object[] { "87000 Wangen,Wangener Str. 3", 5555, "Ramp 13", "02:30", "Wangen", "+491624389222" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"), "Fussen", 3 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("7b520787-18df-44d4-8be2-292411cbcb68"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"), "Memmingen", 4 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"), "Wangen", 1 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"),
                columns: new[] { "DriverId", "TourName", "TourNumber" },
                values: new object[] { new Guid("86770804-cd07-4471-acca-84e83ad0026b"), "Kempten", 2 });
        }
    }
}
