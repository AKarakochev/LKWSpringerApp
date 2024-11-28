using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedClientImagesAndCompositeKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ClientImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Description of the video or/and images.",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Description of the video or/and images.");

            migrationBuilder.InsertData(
                table: "ClientImages",
                columns: new[] { "Id", "ClientId", "Description", "ImageUrl", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("63a7d3f2-0fc8-4ede-aa4d-81ef68f9601e"), new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "Image of Kempten location.", "images/clients/kempten/1.jpg", null },
                    { new Guid("880ae005-0342-4e67-8733-f340e658a42f"), new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "Image of Memmingen location.", "images/clients/memmingen/1.jpg", null },
                    { new Guid("94d28ff4-a327-46c4-9351-3123308655d1"), new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "Image of Fussen location.", "images/clients/fussen/1.jpg", null },
                    { new Guid("cc69f15d-7970-4e57-b824-c45072ec604c"), new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "Image of Wangen location.", "images/clients/wangen/1.jpg", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientImages",
                keyColumn: "Id",
                keyValue: new Guid("63a7d3f2-0fc8-4ede-aa4d-81ef68f9601e"));

            migrationBuilder.DeleteData(
                table: "ClientImages",
                keyColumn: "Id",
                keyValue: new Guid("880ae005-0342-4e67-8733-f340e658a42f"));

            migrationBuilder.DeleteData(
                table: "ClientImages",
                keyColumn: "Id",
                keyValue: new Guid("94d28ff4-a327-46c4-9351-3123308655d1"));

            migrationBuilder.DeleteData(
                table: "ClientImages",
                keyColumn: "Id",
                keyValue: new Guid("cc69f15d-7970-4e57-b824-c45072ec604c"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ClientImages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "Description of the video or/and images.",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Description of the video or/and images.");
        }
    }
}
