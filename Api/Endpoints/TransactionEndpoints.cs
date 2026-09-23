using BudgetApi.Application.Interfaces;
using BudgetApi.Dtos.Transactions;
using BudgetApi.Models;

namespace BudgetApi.Api.Endpoints;

public static class TransactionEndpoints
{
    public static RouteGroupBuilder MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transactions").WithTags("Transactions");

        group.MapGet("/", async (ITransactionService svc) =>
        {
            var items = await svc.ListAsync();
            return Results.Ok(items.Select(ToResponse));
        })
        .WithName("ListTransactions")
        .Produces<IEnumerable<TransactionResponseDto>>();

        group.MapGet("/{id:guid}", async (Guid id, ITransactionService svc) =>
        {
            var tx = await svc.GetAsync(id);
            return tx is null ? Results.NotFound() : Results.Ok(ToResponse(tx));
        })
        .WithName("GetTransaction")
        .Produces<TransactionResponseDto>()
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateTransactionDto dto, ITransactionService svc) =>
        {
            var created = await svc.CreateAsync(dto);
            return Results.Created($"/api/transactions/{created.Id}", ToResponse(created));
        })
        .WithName("CreateTransaction")
        .Produces<TransactionResponseDto>(StatusCodes.Status201Created);

        group.MapPut("/{id:guid}", async (Guid id, UpdateTransactionDto dto, ITransactionService svc) =>
        {
            var updated = await svc.UpdateAsync(id, dto);
            return updated is null ? Results.NotFound() : Results.Ok(ToResponse(updated));
        })
        .WithName("UpdateTransaction")
        .Produces<TransactionResponseDto>()
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", async (Guid id, ITransactionService svc) =>
        {
            var ok = await svc.DeleteAsync(id);
            return ok ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteTransaction")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        return group;
    }

    private static TransactionResponseDto ToResponse(Transaction t) =>
        new(t.Id, t.Timestamp, t.Type, t.Description, t.Amount, t.Date);
}