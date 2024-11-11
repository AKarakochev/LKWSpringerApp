using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTheDriverControllerAndSeedingToursAndClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("e192070e-3c2a-4832-be38-0b9c00d2adc2"));

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "AddressUrl", "ClientNumber", "DeliveryDescription", "DeliveryTime", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "87435 Memmingen,Memmingener Str. 4", null, 110, "Behind the restaurant", "10:00", false, "Memmingen", "+491624389333" },
                    { new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "87629 Fussen,Fussenner Str. 2", null, 3000, "Hospital main entrance", "06:00", false, "Fussen", "+491624389111" },
                    { new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "87435 Kempten,Kemptener Str. 1", null, 101, "Front door", "04:00", false, "Kempten", "+491624389000" },
                    { new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "87000 Wangen,Wangener Str. 3", null, 5555, "Ramp 13", "02:30", false, "Wangen", "+491624389222" }
                });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"),
                columns: new[] { "BirthDate", "FirstName", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[] { new DateTime(1970, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", "00491624490000", "Mustermann", false, true, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("86770804-cd07-4471-acca-84e83ad0026b"),
                columns: new[] { "BirthDate", "FirstName", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[] { new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", "00491624494949", "Schneider", true, false, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "BirthDate", "FirstName", "IsDeleted", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[] { new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"), new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", false, "00491624411111", "Fischer", false, true, new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "DriverId", "IsDeleted", "TourName", "TourNumber" },
                values: new object[,]
                {
                    { new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"), new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"), false, "Fussen", 3 },
                    { new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"), new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"), false, "Wangen", 1 },
                    { new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"), new Guid("86770804-cd07-4471-acca-84e83ad0026b"), false, "Kempten", 2 },
                    { new Guid("7b520787-18df-44d4-8be2-292411cbcb68"), new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"), false, "Memmingen", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("162abc8f-af39-415d-956d-c288a4f401d4"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"));

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"));

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("7b520787-18df-44d4-8be2-292411cbcb68"));

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"));

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"));

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"),
                columns: new[] { "BirthDate", "FirstName", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[] { new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", "00491624494949", "Schneider", true, false, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("86770804-cd07-4471-acca-84e83ad0026b"),
                columns: new[] { "BirthDate", "FirstName", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[] { new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", "00491624411111", "Fischer", false, true, new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "BirthDate", "FirstName", "IsDeleted", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[] { new Guid("e192070e-3c2a-4832-be38-0b9c00d2adc2"), new DateTime(1970, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", false, "00491624490000", "Mustermann", false, true, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
