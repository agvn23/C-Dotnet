using OneDayApi.Api.Filters;
using OneDayApi.Application.Interfaces;
using OneDayApi.Dtos.Author;

namespace OneDayApi.Api.Endpoints;

public static class AuthorEndpoints
{
    public static IEndpointRouteBuilder MapAuthors(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/authors")
            .WithTags("Authors");

        group.MapGet("/", (IAuthorService service) =>
                TypedResults.Ok(service.GetAll()))
            .WithSummary("List all authors")
            .Produces<IEnumerable<AuthorResponseDto>>();

        group.MapGet("/{id:guid}", (Guid id, IAuthorService service) =>
                service.GetById(id) is { } author
                    ? Results.Ok(author)
                    : Results.NotFound())
            .WithSummary("Get an author by ID")
            .Produces<AuthorResponseDto>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/{id:guid}/books", (Guid id, IAuthorService authors, IBookService books) =>
                authors.Exists(id)
                    ? Results.Ok(books.GetByAuthor(id))
                    : Results.NotFound())
            .WithSummary("List books by author")
            .Produces<IEnumerable<Dtos.Book.BookResponseDto>>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", (CreateAuthorDto dto, IAuthorService service) =>
            {
                var created = service.Create(dto);
                return TypedResults.Created($"/authors/{created.Id}", created);
            })
            .WithSummary("Create a new author")
            .Produces<AuthorResponseDto>()
            .ProducesValidationProblem();

        return app;
    }
}