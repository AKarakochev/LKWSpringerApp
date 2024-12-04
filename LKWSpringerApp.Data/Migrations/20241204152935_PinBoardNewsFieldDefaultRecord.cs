using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class PinBoardNewsFieldDefaultRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("02bf54e2-e37f-49c2-b40f-6d2fdb19805b"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("a9523a54-02e8-4dc0-8f08-183558a32f62"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("aa44e1c6-b09a-479e-8c5b-96bf18e71a8d"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("cb142f18-2a6d-49d3-b5df-3170cb5a3b23"));

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "ClientId", "Description", "ImageUrl", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("0f7c35dd-c9c8-4243-81bc-f063c0e1b82c"), new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "Image of Wangen location.", "media/clients/wangen/1.jpg", null },
                    { new Guid("679b08fa-1f84-4118-9845-9fa0b73e466b"), new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "Image of Memmingen location.", "media/clients/memmingen/1.jpg", null },
                    { new Guid("a3e17a9e-a145-452c-b252-dfefb8a765f3"), new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "Image of Kempten location.", "media/clients/kempten/1.jpg", "media/clients/kempten/video2.mp4" },
                    { new Guid("d0679773-11d9-4cf0-a378-7259782ca702"), new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "Image of Fussen location.", "media/clients/fussen/1.jpg", "media/clients/fussen/video1.mp4" }
                });

            migrationBuilder.InsertData(
                table: "PinBoards",
                columns: new[] { "Id", "DriverId", "DrivingCardExpDate", "DrivingCardRenewalDate", "DrivingLicenseExpDate", "DrivingLicenseRenewalDate", "ImportantNews", "News", "UpcomingCourse", "UpcomingCourseDate" },
                values: new object[] { new Guid("2874a397-20f6-46cb-877e-2609b75dfb62"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Keep an eye on this section for updates and important news.", "Welcome to the Driver's PinBoard!", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("0f7c35dd-c9c8-4243-81bc-f063c0e1b82c"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("679b08fa-1f84-4118-9845-9fa0b73e466b"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("a3e17a9e-a145-452c-b252-dfefb8a765f3"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("d0679773-11d9-4cf0-a378-7259782ca702"));

            migrationBuilder.DeleteData(
                table: "PinBoards",
                keyColumn: "Id",
                keyValue: new Guid("2874a397-20f6-46cb-877e-2609b75dfb62"));

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "ClientId", "Description", "ImageUrl", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("02bf54e2-e37f-49c2-b40f-6d2fdb19805b"), new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "Image of Wangen location.", "media/clients/wangen/1.jpg", null },
                    { new Guid("a9523a54-02e8-4dc0-8f08-183558a32f62"), new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "Image of Memmingen location.", "media/clients/memmingen/1.jpg", null },
                    { new Guid("aa44e1c6-b09a-479e-8c5b-96bf18e71a8d"), new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "Image of Kempten location.", "media/clients/kempten/1.jpg", "media/clients/kempten/video2.mp4" },
                    { new Guid("cb142f18-2a6d-49d3-b5df-3170cb5a3b23"), new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "Image of Fussen location.", "media/clients/fussen/1.jpg", "media/clients/fussen/video1.mp4" }
                });
        }
    }
}
