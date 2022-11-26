using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApplication.DAL.Core;
using TestApplication.DAL.Interfaces;
using TestApplication.DAL.Repositories;

namespace TestApplication.DAL.Extensions
{
    public static class Dependencies
    {
        public static void RegisterDependenciesForDAL(this IServiceCollection services, IConfiguration configuration, string connectionName)
        {
            var connection = configuration.GetConnectionString(connectionName);

            services.AddDbContext<TestApplicationContext>(options => options.UseSqlServer(connection));

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        }
    }
}
