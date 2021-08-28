using GreenFlux.SmartCharging.Application;
using GreenFlux.SmartCharging.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux.SmartCharging.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSmartChargingApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddSmartChargingData(configuration);
            return services;
        }
    }
}

