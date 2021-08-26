using GreenFlux.SmartCharging.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux.SmartCharging.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSmartChargingData(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("SmartChargingDbContext");
            services.AddDbContext<SmartChargingDbContext>(options =>
                options.UseSqlServer(dbConnectionString)
            );

            return services;
        }
    }
}
