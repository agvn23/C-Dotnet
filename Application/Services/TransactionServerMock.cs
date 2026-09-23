using BudgetApi.Application.Interfaces;
using BudgetApi.Dtos.Transactions;
using BudgetApi.Models;

namespace BudgetApi.Application.Services;

public class TransactionServiceMock : ITransactionService
{
    private readonly List<Transaction> _store = new();
    private readonly object _lock = new();

    public TransactionServiceMock()
    {
        Seed();
    }

    private void Seed()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        _store.AddRange(new[]
        {
            new Transaction { Type = TransactionType.Income,  Description = "Salary",       Amount = 4200m, Date = today.AddDays(-10) },
            new Transaction { Type = TransactionType.Expense, Description = "Rent",         Amount = 1500m, Date = today.AddDays(-9) },
            new Transaction { Type = TransactionType.Expense, Description = "Groceries",    Amount = 220.50m, Date = today.AddDays(-5) },
            new Transaction { Type = TransactionType.Income,  Description = "Freelance",    Amount = 800m,  Date = today.AddDays(-3) },
            new Transaction { Type = TransactionType.Expense, Description = "Utilities",    Amount = 130m,  Date = today.AddDays(-1) },
        });
    }

    public Task<IEnumerable<Transaction>> ListAsync()
    {
        lock (_lock) return Task.FromResult<IEnumerable<Transaction>>(_store.ToList());
    }

    public Task<Transaction?> GetAsync(Guid id)
    {
        lock (_lock) return Task.FromResult(_store.FirstOrDefault(t => t.Id == id));
    }

    public Task<Transaction> CreateAsync(CreateTransactionDto dto)
    {
        var tx = new Transaction
        {
            Type = dto.Type,
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Timestamp = DateTimeOffset.UtcNow
        };
        lock (_lock) _store.Add(tx);
        return Task.FromResult(tx);
    }

    public Task<Transaction?> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        lock (_lock)
        {
            var tx = _store.FirstOrDefault(t => t.Id == id);
            if (tx is null) return Task.FromResult<Transaction?>(null);
            if (dto.Description is not null) tx.Description = dto.Description;
            if (dto.Amount is not null) tx.Amount = dto.Amount.Value;
            return Task.FromResult<Transaction?>(tx);
        }
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        lock (_lock)
        {
            var tx = _store.FirstOrDefault(t => t.Id == id);
            if (tx is null) return Task.FromResult(false);
            _store.Remove(tx);
            return Task.FromResult(true);
        }
    }
}