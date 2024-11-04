using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LKWSpringerApp.Services.Mapping;
using LKWSpringerApp.Web.ViewModels;
using LKWSpringerApp.Data;
using LKWSpringerApp.Data.Models;
using Microsoft.Extensions.Options;

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

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LkwSpringerDbContext>();
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
               .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
               {
                   ConfigureIdentity(builder, cfg);
               })
               .AddEntityFrameworkStores<LkwSpringerDbContext>()
               .AddSignInManager<SignInManager<ApplicationUser>>()
               .AddUserManager<UserManager<ApplicationUser>>();

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

        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
        {
            cfg.Password.RequireDigit =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireLowercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            cfg.Password.RequireUppercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            cfg.Password.RequireNonAlphanumeric =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
            cfg.Password.RequiredLength =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            cfg.Password.RequiredUniqueChars =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            cfg.SignIn.RequireConfirmedEmail =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail =
                builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}
