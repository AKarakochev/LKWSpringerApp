using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LKWSpringerApp.Services.Mapping;
using LKWSpringerApp.Web.ViewModels;
using LKWSpringerApp.Data;
using LKWSpringerApp.Web.Infrastructure.Extensions;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Repository;
using Microsoft.Extensions.Options;
using LKWSpringerApp.Data.Models.Repository.Interfaces;

namespace LKWSpringerApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SQLServer") ?? throw new InvalidOperationException("Connection string 'SQLServer' not found.");

            builder.Services
                .AddDbContext<LkwSpringerDbContext>(options =>
                {
                    options.UseSqlServer(connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly("LKWSpringerApp.Data"));
                });

            builder.Services
                .AddDefaultIdentity<IdentityUser>
                (options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 6;

                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    options.User.RequireUniqueEmail = true;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                })

                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LkwSpringerDbContext>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddUserManager<UserManager<IdentityUser>>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            //builder.Services.AddScoped<IRepository<Driver, Guid>, BaseRepository<Driver, Guid>>();
            //builder.Services.AddScoped<IRepository<Tour, Guid>, BaseRepository<Tour, Guid>>();
            //builder.Services.AddScoped<IRepository<Client, Guid>, BaseRepository<Client, Guid>>();
            //builder.Services.AddScoped<IRepository<ClientImage, Guid>, BaseRepository<ClientImage, Guid>>();

            builder.Services.RegisterRepositories(typeof(ApplicationUserDriver).Assembly);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

            //Call the seed method for IdentityRole
            //using (var scope = app.Services.CreateScope())
            //{
            //    var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
            //    await seeder.SeedRolesAndAdminUserAsync();
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
