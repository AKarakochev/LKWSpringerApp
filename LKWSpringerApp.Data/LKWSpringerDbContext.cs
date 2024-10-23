using System.Reflection;
using LKWSpringerApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Data
{
    public class LkwSpringerDbContext : IdentityDbContext
    {
        public LkwSpringerDbContext(DbContextOptions<LkwSpringerDbContext> options) 
            : base(options)
        {
            
        }

        

        //1:42 ot parvoto uprajnenie EXERCISE:ASP NET CORE INTRODUCTION
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
