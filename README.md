LINQ (Language Integrated Query) in C#
What is LINQ?
LINQ is a set of features in C# that lets you query collections, databases, XML, and other data sources using a unified, SQL-like syntax directly in your C# code. It was introduced in .NET 3.5 (2007).
The core idea: write queries against any data source using the same syntax, whether it's an in-memory list, a SQL database, an XML document, or an API.
Two Syntax Styles
1. Query syntax (SQL-like, more readable for complex queries):
CopyCopyvar results = from n in numbers
              where n > 5
              orderby n descending
              select n * 2;

2. Method syntax (fluent, uses extension methods + lambdas):
CopyCopyvar results = numbers
    .Where(n => n > 5)
    .OrderByDescending(n => n)
    .Select(n => n * 2);

Both compile to the same thing. Method syntax is more common in modern code; query syntax is often clearer for joins and complex filtering.
Common Operations
CopyCopyvar people = new List<Person> { /* ... */ };

// Filtering
var adults = people.Where(p => p.Age >= 18);

// Projection (transform shape)
var names = people.Select(p => p.Name);

// Ordering
var sorted = people.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

// Grouping
var byCity = people.GroupBy(p => p.City);

// Aggregation
int count = people.Count();
var oldest = people.Max(p => p.Age);
double avg = people.Average(p => p.Age);

// Joining
var joined = from p in people
             join o in orders on p.Id equals o.PersonId
             select new { p.Name, o.Total };

// Set operations
var unique = list1.Distinct();
var common = list1.Intersect(list2);
var combined = list1.Union(list2);

// Quantifiers
bool any = people.Any(p => p.Age > 65);
bool all = people.All(p => p.Age >= 0);

// Element access
var first = people.First();
var firstOrDefault = people.FirstOrDefault(); // null if empty
var single = people.Single(p => p.Id == 5);   // throws if 0 or >1 match

LINQ Providers
The same query syntax works across different providers:



Provider
Data Source
Library




LINQ to Objects
In-memory collections
System.Linq


LINQ to Entities
Databases (EF Core)
Microsoft.EntityFrameworkCore


LINQ to SQL
SQL Server (legacy)
System.Data.Linq


LINQ to XML
XML documents
System.Xml.Linq


PLINQ
Parallel execution
System.Linq.Parallel



CopyCopy// LINQ to Objects — runs in memory
var local = list.Where(x => x > 5);

// LINQ to Entities — translated to SQL, runs on the DB server
var remote = dbContext.Users.Where(u => u.Age > 18).ToList();

Deferred Execution (Important!)
LINQ queries are lazy — they don't run until you enumerate them:
CopyCopyvar query = people.Where(p => p.Age > 18); // nothing happens yet

// Now it runs:
foreach (var p in query) { ... }

// Or force execution:
var list = query.ToList();
var array = query.ToArray();
int count = query.Count();

This means modifying the source collection before enumeration affects the results. It also means you can build queries up in stages efficiently.
Benefits

Unified syntax — one query language for objects, databases, XML, etc.
Compile-time checking — errors caught by the compiler, not at runtime (unlike SQL strings). IntelliSense works.
Type safety — strongly typed results; no casting from DataTable.
Readability — declarative "what" instead of imperative "how" loops.
Composability — chain operations; build queries incrementally.
Less boilerplate — replaces many foreach loops and manual filtering.
Deferred execution — efficient; queries only run when needed.
Provider optimization — with EF Core, LINQ translates to optimized SQL.

Drawbacks / Gotchas

Performance overhead for simple loops — LINQ has delegate/allocation cost. For tight numeric loops, a plain for can be faster.
Deferred execution surprises — forgetting .ToList() can cause repeated execution or unexpected results.
N+1 queries with EF Core if you're not careful (lazy loading, missing Include).
Translation limits — EF Core can't translate every C# expression to SQL; you may get runtime exceptions.
Debugging can be harder with deeply chained queries.
Overuse — not every loop should become a LINQ chain; sometimes a simple loop is clearer.

When to Use It
Good fit:

Querying collections, databases, XML
Filtering/transforming/aggregating data
Complex joins and groupings
EF Core database access (this is the standard way)

Consider alternatives:

Hot paths where every allocation matters (use for/Span)
Very simple single operations
When you need tight control over execution

Quick Example
CopyCopypublic record Employee(string Name, string Dept, int Salary);

var employees = new List<Employee>
{
    new("Alice", "Eng", 120000),
    new("Bob",   "Eng", 95000),
    new("Carol", "Sales", 80000),
    new("Dave",  "Sales", 105000),
};

// Average salary per department, highest first
var report = employees
    .GroupBy(e => e.Dept)
    .Select(g => new
    {
        Dept = g.Key,
        Avg = g.Average(e => e.Salary),
        Count = g.Count()
    })
    .OrderByDescending(x => x.Avg);

foreach (var r in report)
    Console.WriteLine($"{r.Dept}: avg {r.Avg:C}, {r.Count} people");
// Eng: avg $107,500.00, 2 people
// Sales: avg $92,500.00, 2 people

Want me to go deeper on any part — deferred execution, EF Core translation, PLINQ, or expression trees?
 
