using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.AspNetCore.Identity;


namespace DeskMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            var user1 = new IdentityUser
            {
                Id = "06a0da05-4438-4158-a9ca-b18e7778482a",
                UserName = "anastas.karakochev",
                NormalizedUserName = "ANASTAS.KARAKOCHEV",
                Email = "karakochev@example.com",
                NormalizedEmail = "KARAKOCHEV@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var user2 = new IdentityUser
            {
                Id = "53566310-f89b-4c07-abf5-87fdc475fad1",
                UserName = "max.mustermann",
                NormalizedUserName = "MAX.MUSTERMANN",
                Email = "mustermann@example.com",
                NormalizedEmail = "MUSTERMANN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.Entity<IdentityUser>().HasData(user1, user2);
        }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourClient> TourClients { get; set; }
        public virtual DbSet<ClientImage> ClientImages { get; set; }
    }
}
