using Microsoft.Extensions.DependencyInjection;
using TestApplication.BL.DTO;
using TestApplication.BL.Interfaces;
using TestApplication.BL.Services;
using TestApplication.DAL.Extensions;

namespace TestApplication.BL.Extensions
{
    public static class Dependencies
    {
        public static void RegisterDependenciesForBL(this IServiceCollection services, string connectionString)
        {
            services.RegisterDependenciesForDAL(connectionString);

            services.AddTransient<IService<TableADTO, int>, TableAService>();
            services.AddTransient<IService<TableBDTO, int>, TableBService>();
            services.AddTransient<IService<TableCDTO, int>, TableCService>();
        }
    }
}
