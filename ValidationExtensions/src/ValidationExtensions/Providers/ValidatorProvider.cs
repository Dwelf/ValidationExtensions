using Microsoft.Extensions.DependencyInjection;
using ValidationExtensions.Abstractions;
using ValidationExtensions.Abstractions.Providers;

namespace ValidationExtensions.Providers;

public sealed class ValidatorProvider
    : IValidatorProvider
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorProvider(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidators<T> Get<T>()
    {
        return _serviceProvider
            .GetRequiredService<IValidators<T>>();
    }
}