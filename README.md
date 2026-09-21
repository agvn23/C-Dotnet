# ASP.NET Mini Project

Create a small HTTP API using Minimal APIs that demonstrates good structure, validation, and documentation. You will keep data in-memory (refer to the Blog API exercise and solution), focus on clean separation of concerns, and produce a usable OpenAPI document with a friendly UI for exploration.

## Objective
Implement a Minimal API with a clear directory structure.
Model a simple domain with two related resources.
Add body validation using endpoint filters and data annotations.
Expose OpenAPI docs and provide a Scalar UI viewer.
Keep all persistence in memory.

## Instructions
Choose a domain (examples)

Travel API: destinations, users (users can bookmark destinations).
Journal API: users, entries (entries belong to a user).
Library API: books, authors (books reference an author).

Project setup (commands)

` dotnet new web -o OneDayApi `

` cd OneDayApi `

### Directory structure

```.
├── Api
│   ├── Filters
│   │   ├── ValidationExtensions.cs
│   │   └── ValidationFilter.cs
│   ├── Middleware
│   │   └── (optional)
│   ├── Endpoints
│   │   ├── Resource1Endpoints.cs
│   │   └── Resource2Endpoints.cs
├── Application
│   ├── Interfaces
│   │   ├── IResource1Service.cs
│   │   └── IResource2Service.cs
│   └── Services
│       ├── Resource1Service.cs
│       └── Resource2Service.cs
├── Dtos
│   ├── Resource1
│   │   ├── CreateResource1Dto.cs
│   │   ├── UpdateResource1Dto.cs
│   │   └── Resource1ResponseDto.cs
│   └── Resource2
│       ├── CreateResource2Dto.cs
│       ├── UpdateResource2Dto.cs
│       └── Resource2ResponseDto.cs
├── Models
│   ├── Resource1.cs
│   └── Resource2.cs
├── Program.cs
├── Properties
│   └── launchSettings.json
├── appsettings.Development.json
└── appsettings.json
```

#### Minimal APIs and endpoint grouping
Keep Program.cs thin: register services and map route groups via extension methods like app.MapResource1() and app.MapResource2().

Implement endpoints in Api/Endpoints/Resource1Endpoints.cs and Api/Endpoints/Resource2Endpoints.cs using MapGroup("/resource1") and MapGroup("/resource2").

#### In-memory persistence
Implement Application/Services/*Service.cs to store data in memory (e.g., List<T> or Dictionary<Guid,T>).

Expose CRUD operations through Application/Interfaces/* and inject them into endpoints via DI.

#### Body validation
Use data annotations on Create/Update DTOs (e.g., [Required], [StringLength]).

Create a generic ValidationFilter<T> and an extension .WithValidation<T>() under Api/Filters/.

Apply validation to POST and update endpoints.

#### OpenAPI + Scalar UI
Add Microsoft.AspNetCore.OpenApi.

In Program.cs, call builder.Services.AddOpenApi(); before builder.Build().

In Development, call app.MapOpenApi(); and add Scalar.AspNetCore with app.MapScalarApiReference();.

Use TypedResults or .Produces<T>() and .ProducesProblem() for accurate docs.

#### Endpoints to implement (adjust names for your chosen domain)
GET /resource1 list

GET /resource1/{id} by id

POST /resource1 create

PATCH /resource1/{id} or PUT /resource1/{id} update

DELETE /resource1/{id} delete

Relationship route: e.g., GET /resource1/{id}/resource2 (list related items)

Repeat CRUD for resource2

#### Documentation details
Add .WithSummary() and .WithDescription() to key endpoints.

Declare success and error responses using .Produces<T>(status) and .ProducesProblem(status).
