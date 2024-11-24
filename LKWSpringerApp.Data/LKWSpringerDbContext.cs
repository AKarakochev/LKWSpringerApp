using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Configuration;


namespace LKWSpringerApp.Data
{
    public class LkwSpringerDbContext : IdentityDbContext
    {
        public LkwSpringerDbContext()
        {
        }

        public LkwSpringerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<DriverTour> DriverTours { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourClient> TourClients { get; set; }
        public virtual DbSet<ClientImage> ClientImages { get; set; }
        public virtual DbSet<ApplicationUserDriver> UsersDrivers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure the many-to-many relationship
            builder.Entity<ApplicationUserDriver>()
                .HasKey(ud => new { ud.UserId, ud.DriverId });  // Composite key

            builder.Entity<ApplicationUserDriver>()
                .HasOne(ud => ud.User)
                .WithMany()  // IdentityUser doesn't need a collection of drivers
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // Optional, for cascade delete behavior

            builder.Entity<ApplicationUserDriver>()
                .HasOne(ud => ud.Driver)
                .WithMany()  // Driver doesn't need a collection of users
                .HasForeignKey(ud => ud.DriverId)  // Use the explicitly named foreign key
                .OnDelete(DeleteBehavior.Cascade);   // Optional, for cascade delete behavior

            builder.Entity<DriverTour>()
                .HasKey(dt => new { dt.DriverId, dt.TourId });

            builder.Entity<DriverTour>()
                .HasOne(dt => dt.Driver)
                .WithMany(d => d.DriverTours)
                .HasForeignKey(dt => dt.DriverId);

            builder.Entity<DriverTour>()
                .HasOne(dt => dt.Tour)
                .WithMany(t => t.DriverTours)
                .HasForeignKey(dt => dt.TourId);
        }
    }
}

    //var user1 = new ApplicationUser
    //{
    //    Id = Guid.Parse("06a0da05-4438-4158-a9ca-b18e7778482a"),
    //    UserName = "anastas.karakochev",
    //    NormalizedUserName = "ANASTAS.KARAKOCHEV",
    //    Email = "karakochev@example.com",
    //    NormalizedEmail = "KARAKOCHEV@EXAMPLE.COM",
    //    EmailConfirmed = true,
    //    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!"),
    //    SecurityStamp = Guid.NewGuid().ToString()
    //};

    //var user2 = new ApplicationUser
    //{
    //    Id = Guid.Parse("53566310-f89b-4c07-abf5-87fdc475fad1"),
    //    UserName = "max.mustermann",
    //    NormalizedUserName = "MAX.MUSTERMANN",
    //    Email = "mustermann@example.com",
    //    NormalizedEmail = "MUSTERMANN@EXAMPLE.COM",
    //    EmailConfirmed = true,
    //    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!"),
    //    SecurityStamp = Guid.NewGuid().ToString()
    //};

    //builder.Entity<ApplicationUser>().HasData(user1, user2);
