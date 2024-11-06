using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LKWSpringerApp.Services.Mapping;
using LKWSpringerApp.Web.ViewModels;
using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;

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
                (options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LkwSpringerDbContext>();
            

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Identity/Account/Login";
            });


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
