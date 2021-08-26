using FluentValidation;
using GreenFlux.SmartCharging.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux.SmartCharging.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSmartChargingApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(DependencyInjection));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddSmartChargingData(configuration);
            return services;
        }
    }
}

