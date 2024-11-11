using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyReletionshipBetweenTourAndDriverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Drivers_DriverId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_DriverId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Tours");

            migrationBuilder.CreateTable(
                name: "DriverTours",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTours", x => new { x.DriverId, x.TourId });
                    table.ForeignKey(
                        name: "FK_DriverTours_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverTours_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DriverTours",
                columns: new[] { "DriverId", "TourId" },
                values: new object[,]
                {
                    { new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"), new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542") },
                    { new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"), new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f") },
                    { new Guid("86770804-cd07-4471-acca-84e83ad0026b"), new Guid("7b520787-18df-44d4-8be2-292411cbcb68") },
                    { new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"), new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverTours_TourId",
                table: "DriverTours",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverTours");

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"),
                column: "DriverId",
                value: new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("7b520787-18df-44d4-8be2-292411cbcb68"),
                column: "DriverId",
                value: new Guid("86770804-cd07-4471-acca-84e83ad0026b"));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"),
                column: "DriverId",
                value: new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"),
                column: "DriverId",
                value: new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"));

            migrationBuilder.CreateIndex(
                name: "IX_Tours_DriverId",
                table: "Tours",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Drivers_DriverId",
                table: "Tours",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
