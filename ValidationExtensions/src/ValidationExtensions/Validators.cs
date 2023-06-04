using FluentValidation;
using FluentValidation.Results;
using ValidationExtensions.Abstractions;

namespace ValidationExtensions;

public sealed class Validators<T>
    : IValidators<T>
{
    private readonly IEnumerable<IValidator<T>> _validators;

    public Validators(IEnumerable<IValidator<T>> validators)
    {
        _validators = validators;
    }

    public ValidationResult Validate(
        T instance)
    {
        var validationResults
            = _validators
                .Select(x => x.Validate(instance));

        return AggregateValidationResults(
            validationResults);
    }

    public async Task<ValidationResult> ValidateAsync(
        T instance,
        CancellationToken cancellation = default)
    {
        var validationTasks
            = _validators
                .Select(x => x.ValidateAsync(instance, cancellation));

        var validationResults
            = await Task
                .WhenAll(validationTasks);

        return AggregateValidationResults(
            validationResults);
    }

    private static ValidationResult AggregateValidationResults(
        IEnumerable<ValidationResult> validationResults)
    {
        var ruleSetsExecuted
            = new List<string>();

        var validationFailures
            = new List<ValidationFailure>();

        foreach (var validationResult in validationResults)
        {
            ruleSetsExecuted
                .AddRange(validationResult.RuleSetsExecuted);

            validationFailures
                .AddRange(validationResult.Errors);
        }

        return new ValidationResult(validationFailures)
        {
            RuleSetsExecuted = ruleSetsExecuted.ToArray()
        };
    }
}