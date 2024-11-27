using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Data.Repository;

namespace LKWSpringerApp.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type[] typesToExclude = { typeof(ApplicationUserDriver) }; // Optionally exclude models
            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == "LKWSpringerApp.Data.Models") // Adjust namespace
                .ToArray();

            foreach (var type in modelTypes)
            {
                if (typesToExclude.Contains(type)) continue;

                var idProperty = type.GetProperty("Id") ?? type.GetProperty("UserId");
                if (idProperty == null)
                {
                    Console.WriteLine($"Skipping {type.Name}: No 'Id' or 'UserId' property found.");
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
