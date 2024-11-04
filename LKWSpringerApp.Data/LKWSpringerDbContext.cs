using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LKWSpringerApp.Data.Models;


namespace LKWSpringerApp.Data
{
    public class LkwSpringerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public LkwSpringerDbContext()
        {

        }
        public LkwSpringerDbContext(DbContextOptions<LkwSpringerDbContext> options) 
            : base(options)
        {
            
        }

        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourClient> TourClients { get; set; }
        public virtual DbSet<ClientImage> ClientImages { get; set; }
        public virtual DbSet<ApplicationUserDriver> UsersDrivers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


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
        }
    }
}
