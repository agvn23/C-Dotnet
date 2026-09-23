namespace BudgetApi.Dtos.Reports;

public record SummaryLineItemDto(
    DateOnly Date,
    decimal Income,
    decimal Expense,
    decimal Net);

public record SummaryReportResponseDto(
    DateOnly StartDate,
    DateOnly EndDate,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal Net,
    IReadOnlyList<SummaryLineItemDto> LineItems);