using BudgetApi.Application.Interfaces;
using BudgetApi.Dtos.Reports;

namespace BudgetApi.Api.Endpoints;

public static class ReportEndpoints
{
    public static RouteGroupBuilder MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reports").WithTags("Reports");

        group.MapGet("/summary", async (
            DateOnly start,
            DateOnly end,
            string? type,
            IReportService svc) =>
        {
            var report = await svc.GetSummaryAsync(start, end, type ?? "all");
            return Results.Ok(report);
        })
        .WithName("GetSummaryReport")
        .Produces<SummaryReportResponseDto>();

        return group;
    }
}