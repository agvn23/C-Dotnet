using LinqFilteringSelectingOrdering;

var customers = new List<Customer>
{
    new Customer { Name = "Aisha", City = "Berlin",  Age = 29, Account = new BankAccount { Iban = "DE01", Balance = 1800m, Currency = "EUR", Active = true } },
    new Customer { Name = "Jonas", City = "Munich",  Age = 34, Account = new BankAccount { Iban = "DE02", Balance = 250m,  Currency = "EUR", Active = true } },
    new Customer { Name = "Priya", City = "Berlin",  Age = 41, Account = new BankAccount { Iban = "DE03", Balance = 5200m, Currency = "EUR", Active = true } },
    new Customer { Name = "Luca",  City = "Hamburg", Age = 23, Account = new BankAccount { Iban = "DE04", Balance = 0m,    Currency = "EUR", Active = false } },
    new Customer { Name = "Noor",  City = "Leipzig", Age = 37, Account = new BankAccount { Iban = "DE05", Balance = 950m,  Currency = "EUR", Active = true } },
    new Customer { Name = "Omar",  City = "Berlin",  Age = 31, Account = new BankAccount { Iban = "DE06", Balance = 1400m, Currency = "EUR", Active = true } },
};

// Filtering
// All customers in Berlin with an active account
Console.WriteLine("=== Berlin customers with active account ===");

var berlinActive = customers
    .Where(c => c.City == "Berlin" && c.Account.Active);

foreach (var c in berlinActive)
    Console.WriteLine($"{c.Name} ({c.City}) - active: {c.Account.Active}");

// Customers aged between 25 and 40 (inclusive)
Console.WriteLine("\n=== Age 25–40 ===");

var ageRange = customers
    .Where(c => c.Age >= 25 && c.Age <= 40);

foreach (var c in ageRange)
    Console.WriteLine($"{c.Name} - {c.Age}");

// Customers with balance ≥ 1,000 EUR
Console.WriteLine("\n=== Balance >= 1000 EUR ===");

var highBalance = customers
    .Where(c => c.Account.Currency == "EUR" && c.Account.Balance >= 1000m);

foreach (var c in highBalance)
    Console.WriteLine($"{c.Name}: {c.Account.Balance:C}");

// Customers whose name contains the letter 'a' (case-insensitive)
Console.WriteLine("\n=== Name contains 'a' (case-insensitive) ===");

var containsA = customers
    .Where(c => c.Name.Contains('a', StringComparison.OrdinalIgnoreCase));

foreach (var c in containsA)
    Console.WriteLine(c.Name); 

// Selecting (projection)
// A list of customer names only
Console.WriteLine("\n=== Names only ===");

var names = customers.Select(c => c.Name).ToList();

foreach (var n in names)
    Console.WriteLine(n);

// Anonymous objects with { Name, City, Balance }
Console.WriteLine("\n=== Anonymous projection ===");

var summaries = customers
    .Select(c => new
    {
        c.Name,
        c.City,
        Balance = c.Account.Balance
    });

foreach (var s in summaries)
    Console.WriteLine($"{s.Name} in {s.City}: {s.Balance:C}");

// Project to a DTO/record CustomerSummary
Console.WriteLine("\n=== CustomerSummary DTO ===");

var dtos = customers
    .Select(c => new CustomerSummary(
        c.Name,
        c.City,
        c.Account.Balance,
        c.Account.Balance >= 1000m))
    .ToList();

foreach (var d in dtos)
    Console.WriteLine($"{d.Name} ({d.City}) - {d.Balance:C} - HighValue: {d.HighValue}");

// Ordering
// Order by balance descending
Console.WriteLine("\n=== By balance desc ===");

var byBalanceDesc = customers
    .OrderByDescending(c => c.Account.Balance);

foreach (var c in byBalanceDesc)
    Console.WriteLine($"{c.Name}: {c.Account.Balance:C}");

// Order by city, then name within each city
Console.WriteLine("\n=== By city, then name ===");

var byCityThenName = customers
    .OrderBy(c => c.City)
    .ThenBy(c => c.Name);

foreach (var c in byCityThenName)
    Console.WriteLine($"{c.City}: {c.Name}");

// Order by age ascending
Console.WriteLine("\n=== By age asc ===");

var byAge = customers.OrderBy(c => c.Age);

foreach (var c in byAge)
    Console.WriteLine($"{c.Name} - {c.Age}");

// (Stretch): Top 3 balances among active Berlin customers

Console.WriteLine("\n=== Top 3 balances: active Berlin customers ===");

var top3 = customers
    .Where(c => c.City == "Berlin" && c.Account.Active)
    .OrderByDescending(c => c.Account.Balance)
    .Select(c => new { c.Name, Balance = c.Account.Balance })
    .Take(3)
    .ToList();

foreach (var item in top3)
    Console.WriteLine($"{item.Name}: {item.Balance:C}");    

