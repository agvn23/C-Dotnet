# Budget API with DB: Part I

Turn your console-based Budget Tracker into a web API. In Part I you will scaffold a Minimal API, expose OpenAPI docs with a Scalar UI, define core domain models for budgeting, and stub application services that return mocked data. In Part II you will replace the mock layer with a real database using EF Core.

## Objective
Create a Minimal API project for a Budget service
Add native OpenAPI generation and Scalar UI
Define Budget domain models and DTOs
Scaffold application services that return in-memory mocked data
Map clean endpoints for transactions and reports

## Instructions
Bootstrap a new webapi project with dotnet
Directory structure
.
```
├── Api
│   ├── Endpoints
│   │   ├── TransactionEndpoints.cs
│   │   └── ReportEndpoints.cs
│   └── Filters                // keep empty for Part I, used in Part II for validation
├── Application
│   ├── Interfaces
│   │   ├── ITransactionService.cs
│   │   └── IReportService.cs
│   └── Services
│       ├── TransactionServiceMock.cs
│       └── ReportServiceMock.cs
├── Dtos
│   ├── Transactions
│   │   ├── CreateTransactionDto.cs
│   │   ├── UpdateTransactionDto.cs
│   │   └── TransactionResponseDto.cs
│   └── Reports
│       └── SummaryReportResponseDto.cs
├── Models
│   ├── Transaction.cs
│   └── TransactionType.cs
├── Program.cs
├── Properties
│   └── launchSettings.json
├── appsettings.Development.json
└── appsettings.json
```

Models

Transaction has Id: Guid, Timestamp: DateTimeOffset, Type: TransactionType, 
Description: string, Amount: decimal, Date: DateOnly (optional if you want 
to separate logical date from timestamp)

TransactionType enum has Income and Expense

DTOs

CreateTransactionDto: Type, Description, Amount, optional Date
UpdateTransactionDto: optional Description, optional Amount
TransactionResponseDto: mirrors Transaction for responses
SummaryReportResponseDto: StartDate, EndDate, TotalIncome, TotalExpense, Net, and an array of line items if desired
Application services Interfaces (suggestion):

```
// Application/Interfaces/ITransactionService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dtos.Transactions;
using Models;


namespace Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> ListAsync();
    Task<Transaction?> GetAsync(Guid id);
    Task<Transaction> CreateAsync(CreateTransactionDto dto);
    Task<Transaction?> UpdateAsync(Guid id, UpdateTransactionDto dto);
    Task<bool> DeleteAsync(Guid id);
}
```

```
// Application/Interfaces/IReportService.cs
using System.Threading.Tasks;
using Dtos.Reports;
using System;

namespace Application.Interfaces;

public interface IReportService
{
    Task<SummaryReportResponseDto> GetSummaryAsync(DateOnly start, DateOnly end, string type);
}
```

TransactionServiceMock and ReportServiceMock hold in-memory lists and produce deterministic data so your OpenAPI UI shows realistic shapes

Endpoints

Group with app.MapGroup("/api/transactions") and app.MapGroup("/api/reports")

Transactions

GET /api/transactions list

GET /api/transactions/{id} by id

POST /api/transactions create

PUT /api/transactions/{id} update

DELETE /api/transactions/{id} delete

Reports

GET /api/reports/summary?start=YYYY-MM-DD&end=YYYY-MM-DD&type=all|income|expense

Keep handlers thin. Resolve ITransactionService or IReportService from DI and return mocked data from the service

OpenAPI and Scalar UI

Dependency Injection

Register mock services as singleton

```
builder.Services.AddSingleton<ITransactionService, TransactionServiceMock>();
builder.Services.AddSingleton<IReportService, ReportServiceMock>();
```

Launch and verify

dotnet watch run
Visit /scalar/v1 and try each endpoint with realistic payloads
Confirm JSON shapes match DTOs and that report endpoint calculates mock totals
