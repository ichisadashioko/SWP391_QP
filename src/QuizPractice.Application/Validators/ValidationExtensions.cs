using FluentValidation;
using QuizPractice.Application.Common;

namespace QuizPractice.Application.Validators;

public static class ValidationExtensions
{
    /// <summary>Runs the validator and turns its failures into a Validation error, or null when valid.</summary>
    public static async Task<Error?> CheckAsync<T>(this IValidator<T> validator, T request, CancellationToken ct = default)
    {
        var result = await validator.ValidateAsync(request, ct);
        if (result.IsValid)
        {
            return null;
        }

        var fields = result.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Distinct().ToArray());
        return Error.Invalid(fields);
    }
}
