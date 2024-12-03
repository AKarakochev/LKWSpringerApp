﻿// <auto-generated />
using System;
using LKWSpringerApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LKWSpringerApp.Data.Migrations
{
    [DbContext(typeof(LkwSpringerDbContext))]
    partial class LkwSpringerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LKWSpringerApp.Data.Models.ApplicationUserDriver", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "DriverId");

                    b.HasIndex("DriverId");

                    b.ToTable("UsersDrivers");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasComment("The address of the client.");

                    b.Property<string>("AddressUrl")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("The google Url address of the client.");

                    b.Property<int>("ClientNumber")
                        .HasColumnType("int")
                        .HasComment("The number of the client.");

                    b.Property<string>("DeliveryDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasComment("How the client want to make his delivery.");

                    b.Property<string>("DeliveryTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("When the delivery must be made.");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("The name of the client.");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("The phone number of the client.");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("162abc8f-af39-415d-956d-c288a4f401d4"),
                            Address = "87435 Kempten,Kemptener Str. 1",
                            AddressUrl = "https://maps.app.goo.gl/DZyJSoceAaAvx1cN9",
                            ClientNumber = 101,
                            DeliveryDescription = "Front door",
                            DeliveryTime = "04:00",
                            IsDeleted = false,
                            Name = "Kempten",
                            PhoneNumber = "+491624389000"
                        },
                        new
                        {
                            Id = new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"),
                            Address = "87629 Fussen,Fussenner Str. 2",
                            AddressUrl = "https://maps.app.goo.gl/GzDSJXPr1PFcpXmb9",
                            ClientNumber = 3000,
                            DeliveryDescription = "Hospital main entrance",
                            DeliveryTime = "06:00",
                            IsDeleted = false,
                            Name = "Fussen",
                            PhoneNumber = "+491624389111"
                        },
                        new
                        {
                            Id = new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"),
                            Address = "87000 Wangen,Wangener Str. 3",
                            AddressUrl = "https://maps.app.goo.gl/NHu42wMmYcDWMJop6",
                            ClientNumber = 5555,
                            DeliveryDescription = "Ramp 13",
                            DeliveryTime = "02:30",
                            IsDeleted = false,
                            Name = "Wangen",
                            PhoneNumber = "+491624389222"
                        },
                        new
                        {
                            Id = new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"),
                            Address = "87435 Memmingen,Memmingener Str. 4",
                            AddressUrl = "https://maps.app.goo.gl/ZzMGLmmM15hgpEFY9",
                            ClientNumber = 110,
                            DeliveryDescription = "Behind the restaurant",
                            DeliveryTime = "10:00",
                            IsDeleted = false,
                            Name = "Memmingen",
                            PhoneNumber = "+491624389333"
                        });
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasComment("Driver birthdate.");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Driver first name.");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Shows if a driver has been deleted.");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("The phone number of the driver.");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Driver second name.");

                    b.Property<bool>("Springerdriver")
                        .HasColumnType("bit")
                        .HasComment("That is a driver who visits different clients almost every day.");

                    b.Property<bool>("Stammdriver")
                        .HasColumnType("bit")
                        .HasComment("That is a driver who visits the same clients.");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date of commencement of employment.");

                    b.HasKey("Id");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"),
                            BirthDate = new DateTime(1985, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Anastas",
                            IsDeleted = false,
                            PhoneNumber = "00491624389341",
                            SecondName = "Karakochev",
                            Springerdriver = true,
                            Stammdriver = false,
                            StartDate = new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("86770804-cd07-4471-acca-84e83ad0026b"),
                            BirthDate = new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Daniel",
                            IsDeleted = false,
                            PhoneNumber = "00491624494949",
                            SecondName = "Schneider",
                            Springerdriver = true,
                            Stammdriver = false,
                            StartDate = new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"),
                            BirthDate = new DateTime(1970, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Max",
                            IsDeleted = false,
                            PhoneNumber = "00491624490000",
                            SecondName = "Mustermann",
                            Springerdriver = false,
                            Stammdriver = true,
                            StartDate = new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"),
                            BirthDate = new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ben",
                            IsDeleted = false,
                            PhoneNumber = "00491624411111",
                            SecondName = "Fischer",
                            Springerdriver = false,
                            Stammdriver = true,
                            StartDate = new DateTime(2022, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.DriverTour", b =>
                {
                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.HasKey("DriverId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("DriverTours");

                    b.HasData(
                        new
                        {
                            DriverId = new Guid("8654035c-e140-4fc7-b9dd-1a36e2a09186"),
                            TourId = new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f")
                        },
                        new
                        {
                            DriverId = new Guid("86770804-cd07-4471-acca-84e83ad0026b"),
                            TourId = new Guid("7b520787-18df-44d4-8be2-292411cbcb68")
                        },
                        new
                        {
                            DriverId = new Guid("7959723f-22e9-4efb-a334-cf25c5bd9431"),
                            TourId = new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542")
                        },
                        new
                        {
                            DriverId = new Guid("a22dd3bc-24f9-4dea-b986-8a198d460a8f"),
                            TourId = new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a")
                        });
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Description of the video or/and images.");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("ImageUrl of the client location and delivery area.");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("VideoUrl of the client location and delivery area.");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Media", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa44e1c6-b09a-479e-8c5b-96bf18e71a8d"),
                            ClientId = new Guid("162abc8f-af39-415d-956d-c288a4f401d4"),
                            Description = "Image of Kempten location.",
                            ImageUrl = "media/clients/kempten/1.jpg",
                            VideoUrl = "media/clients/kempten/video2.mp4"
                        },
                        new
                        {
                            Id = new Guid("cb142f18-2a6d-49d3-b5df-3170cb5a3b23"),
                            ClientId = new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5"),
                            Description = "Image of Fussen location.",
                            ImageUrl = "media/clients/fussen/1.jpg",
                            VideoUrl = "media/clients/fussen/video1.mp4"
                        },
                        new
                        {
                            Id = new Guid("02bf54e2-e37f-49c2-b40f-6d2fdb19805b"),
                            ClientId = new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87"),
                            Description = "Image of Wangen location.",
                            ImageUrl = "media/clients/wangen/1.jpg"
                        },
                        new
                        {
                            Id = new Guid("a9523a54-02e8-4dc0-8f08-183558a32f62"),
                            ClientId = new Guid("7a80f16d-f7b0-467c-9f96-61d506702150"),
                            Description = "Image of Memmingen location.",
                            ImageUrl = "media/clients/memmingen/1.jpg"
                        });
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.PinBoard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DrivingCardExpDate")
                        .HasColumnType("datetime2")
                        .HasComment("The expiration date of the driving card in MM/YYYY format.");

                    b.Property<DateTime?>("DrivingCardRenewalDate")
                        .HasColumnType("datetime2")
                        .HasComment("The renewal date of the driving card in MM/YYYY format.");

                    b.Property<DateTime>("DrivingLicenseExpDate")
                        .HasColumnType("datetime2")
                        .HasComment("The expiration date of the driving license in MM/YYYY format.");

                    b.Property<DateTime?>("DrivingLicenseRenewalDate")
                        .HasColumnType("datetime2")
                        .HasComment("The renewal date of the driving license in MM/YYYY format.");

                    b.Property<string>("ImportantNews")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Important news for the driver.");

                    b.Property<string>("News")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("General news for the driver.");

                    b.Property<string>("UpcomingCourse")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Details of the upcoming course.");

                    b.Property<DateTime?>("UpcomingCourseDate")
                        .HasColumnType("datetime2")
                        .HasComment("The date of the upcoming course in dd/MM/YYYY format.");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("PinBoards");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Tour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("The name of the tour.");

                    b.Property<int>("TourNumber")
                        .HasColumnType("int")
                        .HasComment("The number of the tour.");

                    b.HasKey("Id");

                    b.ToTable("Tours");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"),
                            IsDeleted = false,
                            TourName = "Wangen",
                            TourNumber = 1
                        },
                        new
                        {
                            Id = new Guid("7b520787-18df-44d4-8be2-292411cbcb68"),
                            IsDeleted = false,
                            TourName = "Kempten",
                            TourNumber = 2
                        },
                        new
                        {
                            Id = new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"),
                            IsDeleted = false,
                            TourName = "Fussen",
                            TourNumber = 3
                        },
                        new
                        {
                            Id = new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"),
                            IsDeleted = false,
                            TourName = "Memmingen",
                            TourNumber = 4
                        });
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.TourClient", b =>
                {
                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier.");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TourId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("TourClients");

                    b.HasData(
                        new
                        {
                            TourId = new Guid("1f500845-25ef-4a18-9fdc-14f69568cf1f"),
                            ClientId = new Guid("162abc8f-af39-415d-956d-c288a4f401d4")
                        },
                        new
                        {
                            TourId = new Guid("7b520787-18df-44d4-8be2-292411cbcb68"),
                            ClientId = new Guid("0ceac7e0-f9d5-45f0-9845-8a58141184d5")
                        },
                        new
                        {
                            TourId = new Guid("cef8eeb6-d07c-42ce-959f-cae8c1fae542"),
                            ClientId = new Guid("47f3539d-42a7-47c2-86f5-67ebf9638b87")
                        },
                        new
                        {
                            TourId = new Guid("a3101694-8d27-4d93-8b76-a2bc7cdeed7a"),
                            ClientId = new Guid("7a80f16d-f7b0-467c-9f96-61d506702150")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.ApplicationUserDriver", b =>
                {
                    b.HasOne("LKWSpringerApp.Data.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.DriverTour", b =>
                {
                    b.HasOne("LKWSpringerApp.Data.Models.Driver", "Driver")
                        .WithMany("DriverTours")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LKWSpringerApp.Data.Models.Tour", "Tour")
                        .WithMany("DriverTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Media", b =>
                {
                    b.HasOne("LKWSpringerApp.Data.Models.Client", "Client")
                        .WithMany("Media")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.PinBoard", b =>
                {
                    b.HasOne("LKWSpringerApp.Data.Models.Driver", "Driver")
                        .WithMany("PinBoards")
                        .HasForeignKey("DriverId");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.TourClient", b =>
                {
                    b.HasOne("LKWSpringerApp.Data.Models.Client", "Client")
                        .WithMany("ClientsTours")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LKWSpringerApp.Data.Models.Tour", "Tour")
                        .WithMany("ToursClients")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Client", b =>
                {
                    b.Navigation("ClientsTours");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Driver", b =>
                {
                    b.Navigation("DriverTours");

                    b.Navigation("PinBoards");
                });

            modelBuilder.Entity("LKWSpringerApp.Data.Models.Tour", b =>
                {
                    b.Navigation("DriverTours");

                    b.Navigation("ToursClients");
                });
#pragma warning restore 612, 618
        }
    }
}
