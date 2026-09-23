using BudgetApi.Application.Interfaces;
using BudgetApi.Dtos.Reports;
using BudgetApi.Models;

namespace BudgetApi.Application.Services;

public class ReportServiceMock : IReportService
{
    private readonly ITransactionService _transactions;

    public ReportServiceMock(ITransactionService transactions)
    {
        _transactions = transactions;
    }

    public async Task<SummaryReportResponseDto> GetSummaryAsync(DateOnly start, DateOnly end, string type)
    {
        var all = await _transactions.ListAsync();
        var filter = (type ?? "all").ToLowerInvariant();

        var inRange = all
            .Where(t => t.Date.HasValue)
            .Where(t => t.Date!.Value >= start && t.Date!.Value <= end)
            .Where(t => filter switch
            {
                "income"  => t.Type == TransactionType.Income,
                "expense" => t.Type == TransactionType.Expense,
                _         => true
            })
            .ToList();

        var totalIncome  = inRange.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var totalExpense = inRange.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

        var lineItems = inRange
            .GroupBy(t => t.Date!.Value)
            .OrderBy(g => g.Key)
            .Select(g =>
            {
                var inc = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                var exp = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                return new SummaryLineItemDto(g.Key, inc, exp, inc - exp);
            })
            .ToList();

        return new SummaryReportResponseDto(start, end, totalIncome, totalExpense, totalIncome - totalExpense, lineItems);
    }
}