using Microsoft.Extensions.DependencyInjection;
using ValidationExtensions.Abstractions;
using ValidationExtensions.Abstractions.Providers;
using ValidationExtensions.Providers;

namespace ValidationExtensions.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddValidatorExtensions(this IServiceCollection services)
    {
        services
            .AddSingleton<IValidatorProvider, ValidatorProvider>();
            
        services
            .AddTransient(typeof(IValidators<>), typeof(IValidators<>));

        return services;
    }
}