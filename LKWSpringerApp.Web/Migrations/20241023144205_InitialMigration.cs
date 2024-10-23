using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the client."),
                    ClientNumber = table.Column<int>(type: "int", nullable: false, comment: "The number of the client is required."),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "The address of the client."),
                    AddressUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The phone number of the client."),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "How the client want we to make his delivery."),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "When the delivery must be made."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Driver first name."),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Driver second name."),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Driver birthdate."),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of commencement of employment."),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The phone number of the driver."),
                    Springerfahrer = table.Column<bool>(type: "bit", nullable: false, comment: "That is a driver who visits different clients almost every day."),
                    Stammfahrer = table.Column<bool>(type: "bit", nullable: false, comment: "That is a driver who visits the same clients."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows if a driver has been deleted.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "ImageUrl of the client location and delivery area."),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "VideoUrl of the client location and delivery area."),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Description of the video or/and images."),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientImages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    TourNumber = table.Column<int>(type: "int", nullable: false, comment: "The number of the tour."),
                    TourName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the tour."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourClients",
                columns: table => new
                {
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourClients", x => new { x.TourId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_TourClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourClients_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "06a0da05-4438-4158-a9ca-b18e7778482a", 0, "abb484ec-5c79-40d7-9095-859cca9adea7", "karakochev@example.com", true, false, null, "KARAKOCHEV@EXAMPLE.COM", "ANASTAS.KARAKOCHEV", "AQAAAAIAAYagAAAAENg5M0wIt3IYJr4GffuFfHHc539874yYABB3gfeBccMnYQGvSx4naJHIE/Of5tLnRQ==", null, false, "f87b9385-9b30-497d-98b2-691273d8f288", false, "anastas.karakochev" },
                    { "53566310-f89b-4c07-abf5-87fdc475fad1", 0, "6681f33d-2761-4223-806a-deac2e79bbe6", "mustermann@example.com", true, false, null, "MUSTERMANN@EXAMPLE.COM", "MAX.MUSTERMANN", "AQAAAAIAAYagAAAAEOMBOQfr56ADuU8xkrEW79K09qB5LKvd+iDPF98bWa6iVIt6GhX2tNNAJd90wiQ3YA==", null, false, "91092d10-dd08-43de-981a-965127dd4b2f", false, "max.mustermann" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "BirthDate", "FirstName", "IsDeleted", "PhoneNumber", "SecondName", "Springerfahrer", "Stammfahrer", "StartDate" },
                values: new object[,]
                {
                    { new Guid("0b225445-d6e9-4dfc-825b-6f02e41721ef"), new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", false, "00491624494949", "Schneider", true, false, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("18ff45e9-0d2d-426d-9dfb-c7bf07141191"), new DateTime(1970, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", false, "00491624490000", "Mustermann", false, true, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5e6e60ee-2981-4e94-8253-6a62866ea7c3"), new DateTime(1985, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anastas", false, "00491624389341", "Karakochev", true, false, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bbd2bec8-22bb-4e5d-bcb8-e3ec0370b1d5"), new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", false, "00491624411111", "Fischer", false, true, new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClientImages_ClientId",
                table: "ClientImages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TourClients_ClientId",
                table: "TourClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_DriverId",
                table: "Tours",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientImages");

            migrationBuilder.DropTable(
                name: "TourClients");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
