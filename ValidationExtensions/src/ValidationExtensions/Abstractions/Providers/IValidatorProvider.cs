namespace ValidationExtensions.Abstractions.Providers;

public interface IValidatorProvider
{
    IValidators<T> Get<T>();
}