using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestApplication.DAL.Core;
using TestApplication.DAL.Interfaces;
using TestApplication.DAL.Repositories;

namespace TestApplication.DAL.Extensions;

public static class Dependencies
{
    public static void RegisterDependenciesForDAL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MyTestApplicationContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
    }
}
