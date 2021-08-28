using GreenFlux.SmartCharging.Domain.Interfaces;
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
            //TODO: dbConnectionString is null. I didn't have time to look into it. 
            services.AddDbContext<SmartChargingDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SmartCharging;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            );
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            return services;
        }
    }
}
