using OneDayApi.Api.Filters;
using OneDayApi.Application.Interfaces;
using OneDayApi.Dtos.Book;

namespace OneDayApi.Api.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBooks(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/books")
            .WithTags("Books");

        group.MapGet("/", (IBookService service) =>
        {
            return Results.Ok(service.GetAll());
        })
        .WithSummary("Get all books")
        .WithDescription("Returns all books currently stored in memory.")
        .Produces<IEnumerable<BookResponseDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id:guid}", (
            Guid id,
            IBookService service) =>
        {
            var book = service.GetById(id);

            return book is null
                ? Results.NotFound()
                : Results.Ok(book);
        })
        .WithSummary("Get a book")
        .WithDescription("Returns a book by its unique identifier.")
        .Produces<BookResponseDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", (
            CreateBookDto dto,
            IBookService bookService,
            IAuthorService authorService) =>
        {
            if (authorService.GetById(dto.AuthorId) is null)
            {
                return Results.Problem(
                    title: "Author not found",
                    detail: "The specified author does not exist.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var book = bookService.Create(dto);

            return Results.Created(
                $"/books/{book.Id}",
                book);
        })
        .WithValidation<CreateBookDto>()
        .WithSummary("Create a book")
        .WithDescription("Creates a new book associated with an existing author.")
        .Produces<BookResponseDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPatch("/{id:guid}", (
            Guid id,
            UpdateBookDto dto,
            IBookService bookService,
            IAuthorService authorService) =>
        {
            if (authorService.GetById(dto.AuthorId) is null)
            {
                return Results.Problem(
                    title: "Author not found",
                    detail: "The specified author does not exist.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var book = bookService.Update(id, dto);

            return book is null
                ? Results.NotFound()
                : Results.Ok(book);
        })
        .WithValidation<UpdateBookDto>()
        .WithSummary("Update a book")
        .WithDescription("Updates an existing book.")
        .Produces<BookResponseDto>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", (
            Guid id,
            IBookService service) =>
        {
            var deleted = service.Delete(id);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        })
        .WithSummary("Delete a book")
        .WithDescription("Deletes a book.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}
