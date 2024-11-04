using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LKWSpringerApp.Data.Models;

namespace LKWSpringerApp.Data.Configuration
{
    public class ApplicationUserDriverConfiguration : IEntityTypeConfiguration<ApplicationUserDriver>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserDriver> builder)
        {
            builder
                .HasKey(k => new { k.ApplicationUserId, k.DriverId });

            builder
                .HasOne(k => k.Driver)
                .WithMany(k => k.DriverApplicationUsers)
                .HasForeignKey(k => k.DriverId);

            builder
                .HasOne(k => k.ApplicationUser)
                .WithMany(u => u.ApplicationUserDrivers)
                .HasForeignKey(k => k.ApplicationUserId);
        }
    }
}
