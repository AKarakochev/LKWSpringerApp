using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDriversAndDriverControllerConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "BirthDate", "FirstName", "IsDeleted", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[,]
                {
                    { new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"), new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", false, "00491624494949", "Schneider", true, false, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"), new DateTime(1985, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anastas", false, "00491624389341", "Karakochev", true, false, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("86770804-cd07-4471-acca-84e83ad0026b"), new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", false, "00491624411111", "Fischer", false, true, new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e192070e-3c2a-4832-be38-0b9c00d2adc2"), new DateTime(1970, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", false, "00491624490000", "Mustermann", false, true, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("86770804-cd07-4471-acca-84e83ad0026b"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("e192070e-3c2a-4832-be38-0b9c00d2adc2"));
        }
    }
}
