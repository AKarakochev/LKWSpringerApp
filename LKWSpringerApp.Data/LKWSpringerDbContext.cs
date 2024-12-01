using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using LKWSpringerApp.Data.Models;
using System.Reflection.Emit;

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
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<ApplicationUserDriver> UsersDrivers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Media>().ToTable("Media");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ApplicationUserDriver>()
                .HasKey(ud => new { ud.UserId, ud.DriverId });

            builder.Entity<ApplicationUserDriver>()
                .HasOne(ud => ud.User)
                .WithMany()
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUserDriver>()
                .HasOne(ud => ud.Driver)
                .WithMany()
                .HasForeignKey(ud => ud.DriverId)
                .OnDelete(DeleteBehavior.Cascade);

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

            builder.Entity<TourClient>()
                .HasKey(tc => new { tc.TourId, tc.ClientId });

            builder.Entity<TourClient>()
                .HasOne(tc => tc.Tour)
                .WithMany(t => t.ToursClients)
                .HasForeignKey(tc => tc.TourId);

            builder.Entity<TourClient>()
                .HasOne(tc => tc.Client)
                .WithMany(c => c.ClientsTours)
                .HasForeignKey(tc => tc.ClientId);

            builder.Entity<Media>()
                .HasOne(m => m.Client)
                .WithMany(c => c.Media)
                .HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
