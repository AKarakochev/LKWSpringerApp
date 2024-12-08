using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Data.Repository;
using Microsoft.AspNetCore.Identity;

namespace LKWSpringerApp.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
           
            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == "LKWSpringerApp.Data.Models")
                .ToArray();

            foreach (var type in modelTypes)
            {
                if (type == typeof(IdentityUser)) continue;

                var idProperty = type.GetProperty("Id");
                if (idProperty == null)
                {
                    Console.WriteLine($"Skipping {type.Name}: No 'Id' property found.");
                    continue;
                }

                var idType = idProperty.PropertyType;
                var repositoryInterface = typeof(IRepository<,>).MakeGenericType(type, idType);
                var repositoryImplementation = typeof(BaseRepository<,>).MakeGenericType(type, idType);

                services.AddScoped(repositoryInterface, repositoryImplementation);
            }
        }
    }
}
