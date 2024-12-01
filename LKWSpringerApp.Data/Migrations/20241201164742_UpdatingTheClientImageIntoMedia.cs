using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LKWSpringerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTheClientImageIntoMedia : Migration
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
                    ClientNumber = table.Column<int>(type: "int", nullable: false, comment: "The number of the client."),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "The address of the client."),
                    AddressUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "The google Url address of the client."),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The phone number of the client."),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "How the client want to make his delivery."),
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
                    Springerdriver = table.Column<bool>(type: "bit", nullable: false, comment: "That is a driver who visits different clients almost every day."),
                    Stammdriver = table.Column<bool>(type: "bit", nullable: false, comment: "That is a driver who visits the same clients."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows if a driver has been deleted.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    TourNumber = table.Column<int>(type: "int", nullable: false, comment: "The number of the tour."),
                    TourName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the tour."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
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
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "ImageUrl of the client location and delivery area."),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "VideoUrl of the client location and delivery area."),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Description of the video or/and images."),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersDrivers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDrivers", x => new { x.UserId, x.DriverId });
                    table.ForeignKey(
                        name: "FK_UsersDrivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersDrivers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverTours",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier."),
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier.")
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
                table: "Clients",
                columns: new[] { "Id", "Address", "AddressUrl", "ClientNumber", "DeliveryDescription", "DeliveryTime", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"), "87629 Fussen,Fussenner Str. 2", "https://maps.app.goo.gl/GzDSJXPr1PFcpXmb9", 3000, "Hospital main entrance", "06:00", false, "Fussen", "+491624389111" },
                    { new Guid("162abc8f-af39-415d-956d-c288a4f401d4"), "87435 Kempten,Kemptener Str. 1", "https://maps.app.goo.gl/DZyJSoceAaAvx1cN9", 101, "Front door", "04:00", false, "Kempten", "+491624389000" },
                    { new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"), "87000 Wangen,Wangener Str. 3", "https://maps.app.goo.gl/NHu42wMmYcDWMJop6", 5555, "Ramp 13", "02:30", false, "Wangen", "+491624389222" },
                    { new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"), "87435 Memmingen,Memmingener Str. 4", "https://maps.app.goo.gl/ZzMGLmmM15hgpEFY9", 110, "Behind the restaurant", "10:00", false, "Memmingen", "+491624389333" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "BirthDate", "FirstName", "IsDeleted", "PhoneNumber", "SecondName", "Springerdriver", "Stammdriver", "StartDate" },
                values: new object[,]
                {
                    { new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"), new DateTime(1970, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", false, "00491624490000", "Mustermann", false, true, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"), new DateTime(1985, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anastas", false, "00491624389341", "Karakochev", true, false, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("86770804-cd07-4471-acca-84e83ad0026b"), new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", false, "00491624494949", "Schneider", true, false, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"), new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", false, "00491624411111", "Fischer", false, true, new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "IsDeleted", "TourName", "TourNumber" },
                values: new object[,]
                {
                    { new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"), false, "Wangen", 1 },
                    { new Guid("7b520787-18df-44d4-8be2-292411cbcb68"), false, "Kempten", 2 },
                    { new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"), false, "Memmingen", 4 },
                    { new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"), false, "Fussen", 3 }
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
                name: "IX_DriverTours_TourId",
                table: "DriverTours",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ClientId",
                table: "Media",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TourClients_ClientId",
                table: "TourClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDrivers_DriverId",
                table: "UsersDrivers",
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
                name: "DriverTours");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "TourClients");

            migrationBuilder.DropTable(
                name: "UsersDrivers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
