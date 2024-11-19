using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using LKWSpringerApp.Data.Models;
using LKWSpringerApp.Data.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using LKWSpringerApp.Data.Repository;

namespace LKWSpringerApp.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type[] typesToExclude = new Type[] { typeof(ApplicationUserDriver) };
            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => !t.IsAbstract  && !t.IsInterface && !t.Name.ToLower().EndsWith("attribute"))
                .ToArray();

            foreach (Type type in modelTypes) 
            {

                if (!typesToExclude.Contains(type))
                {
                    Type repositoryInterface = typeof(IRepository<,>);
                    Type repositoryInstanceType = typeof(BaseRepository<,>);
                    PropertyInfo idPropertyInfo = type
                        .GetProperties()
                        .Where(p => p.Name.ToLower() == "id")
                        .SingleOrDefault();

                    Type[] constructArgs = new Type[2];
                    constructArgs[0] = type;

                    if(idPropertyInfo == null)
                    {
                        constructArgs[1] = typeof(object);
                    }
                    else
                    {
                        constructArgs[1] = idPropertyInfo.GetType();
                    }

                    repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                    services.AddScoped(repositoryInterface, repositoryInstanceType);
                }
            }
        }
    }
}
