using System.ComponentModel.DataAnnotations;

namespace OneDayApi.Api.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var argument = context.Arguments.OfType<T>().FirstOrDefault();
        if (argument is null)
            return Results.BadRequest(new { error = "Request body is required." });

        var validationContext = new ValidationContext(argument);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(argument, validationContext, results, true))
        {
            var errors = results
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "general")
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(r => r.ErrorMessage ?? "Invalid").ToArray());

            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }
}