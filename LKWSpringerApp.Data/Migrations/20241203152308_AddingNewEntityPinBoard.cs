using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewEntityPinBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("2f672697-4b0f-4957-b19b-a06a3d29ead2"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("b8028002-caee-416c-954d-f4192a9865a2"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("d8a3ff6a-6f55-4df8-ba57-fcc785b79b33"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("dcf55204-cfa9-4380-bd6a-860887b02e12"));

            migrationBuilder.CreateTable(
                name: "PinBoards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    DrivingLicenseExpDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The expiration date of the driving license in MM/YYYY format."),
                    DrivingCardExpDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The expiration date of the driving card in MM/YYYY format."),
                    DrivingLicenseRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "The renewal date of the driving license in MM/YYYY format."),
                    DrivingCardRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "The renewal date of the driving card in MM/YYYY format."),
                    UpcomingCourse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Details of the upcoming course."),
                    UpcomingCourseDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "The date of the upcoming course in dd/MM/YYYY format."),
                    News = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "General news for the driver."),
                    ImportantNews = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Important news for the driver."),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinBoards_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "ClientId", "Description", "ImageUrl", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("2e66ea4e-6765-4e1c-9dc5-e04c8e574bbc"), new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "Image of Kempten location.", "media/clients/kempten/1.jpg", "media/clients/kempten/video2.mp4" },
                    { new Guid("781567ce-9745-47a0-a925-cbf92fe37f2c"), new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "Image of Fussen location.", "media/clients/fussen/1.jpg", "media/clients/fussen/video1.mp4" },
                    { new Guid("b76f6a5c-ada7-4840-a932-7656fe3d9fda"), new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "Image of Wangen location.", "media/clients/wangen/1.jpg", null },
                    { new Guid("fe9c0622-b2a8-4119-b473-290a23de891e"), new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "Image of Memmingen location.", "media/clients/memmingen/1.jpg", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PinBoards_DriverId",
                table: "PinBoards",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PinBoards");

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("2e66ea4e-6765-4e1c-9dc5-e04c8e574bbc"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("781567ce-9745-47a0-a925-cbf92fe37f2c"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("b76f6a5c-ada7-4840-a932-7656fe3d9fda"));

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: new Guid("fe9c0622-b2a8-4119-b473-290a23de891e"));

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "ClientId", "Description", "ImageUrl", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("2f672697-4b0f-4957-b19b-a06a3d29ead2"), new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "Image of Fussen location.", "media/clients/fussen/1.jpg", "media/clients/fussen/video1.mp4" },
                    { new Guid("b8028002-caee-416c-954d-f4192a9865a2"), new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "Image of Memmingen location.", "media/clients/memmingen/1.jpg", null },
                    { new Guid("d8a3ff6a-6f55-4df8-ba57-fcc785b79b33"), new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "Image of Kempten location.", "media/clients/kempten/1.jpg", "media/clients/kempten/video2.mp4" },
                    { new Guid("dcf55204-cfa9-4380-bd6a-860887b02e12"), new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "Image of Wangen location.", "media/clients/wangen/1.jpg", null }
                });
        }
    }
}
