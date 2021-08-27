using FluentValidation;
using GreenFlux.SmartCharging.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux.SmartCharging.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
