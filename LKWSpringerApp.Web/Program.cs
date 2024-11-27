using LKWSpringerApp.Services.Mapping;
using LKWSpringerApp.Web.ViewModels;
using LKWSpringerApp.Data;
using LKWSpringerApp.Web.Infrastructure.Extensions;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Services.Data;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Data.Repository;
using LKWSpringerApp.Data.Configuration;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
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
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;

                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.RegisterRepositories(typeof(ApplicationUserDriver).Assembly);
            
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<ITourService, TourService>();
            builder.Services.AddScoped<IRepository<DriverTour, Guid>, BaseRepository<DriverTour, Guid>>();
            builder.Services.AddScoped<IClientImageService, ClientImageService>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddRazorPages();

            var app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                // Exception handler for server errors
                app.UseExceptionHandler("/Error/500");

                // Re-execute middleware for other status codes (e.g., 404)
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy",
                    "default-src 'self'; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline'; img-src 'self' data:;");
                await next();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await RoleConfiguration.SeedRolesAndAdminAsync(services);
            }

            app.Run();
        }
    }
}
