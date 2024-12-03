using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DriverIdAndDriverCanBeNullInPinBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PinBoards_Drivers_DriverId",
                table: "PinBoards");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "PinBoards",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PinBoards_Drivers_DriverId",
                table: "PinBoards",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PinBoards_Drivers_DriverId",
                table: "PinBoards");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "PinBoards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PinBoards_Drivers_DriverId",
                table: "PinBoards",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
