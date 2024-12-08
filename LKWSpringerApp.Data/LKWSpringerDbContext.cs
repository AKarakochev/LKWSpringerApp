using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using LKWSpringerApp.Data.Models;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace LKWSpringerApp.Data
{
    public class LkwSpringerDbContext : IdentityDbContext<IdentityUser>
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
        public virtual DbSet<PinBoard> PinBoards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Media>().ToTable("Media");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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

            builder.Entity<PinBoard>().HasData(
                new PinBoard
                {
                    Id = Guid.NewGuid(),
                    News = "Welcome to the Driver's PinBoard!",
                    ImportantNews = "Keep an eye on this section for updates and important news."
                });
        }
    }
}
