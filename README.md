# Minimal Blog API

Let’s build our first API powered by ASP.NET!

## Objective
Build a small Blog API using Minimal APIs. Declare endpoints via extension methods, use 
DTOs for requests/responses, and keep data in in-memory persistence. A User can own 
many Posts; each Post belongs to exactly one User.

## Setup
```
dotnet new web -o BlogApi
cd BlogApi
code .
```

## Target Directory Structure
```
BlogApi/
├── Program.cs
├── appsettings.json
├── Properties/
│   └── launchSettings.json
├── Models/
│   ├── User.cs
│   └── Post.cs
├── Dtos/
│   ├── Users/
│   │   ├── CreateUserDto.cs
│   │   ├── UpdateUserDto.cs
│   │   └── UserResponseDto.cs
│   └── Posts/
│       ├── CreatePostDto.cs
│       ├── UpdatePostDto.cs
│       └── PostResponseDto.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IUserService.cs
│   │   └── IPostService.cs
│   ├── InMemoryUserService.cs
│   └── InMemoryPostService.cs
└── Endpoints/
    ├── UserEndpoints.cs
    └── PostEndpoints.cs
```

### Folders

Models/: lean domain entities (no serialization/validation attributes required).

Dtos/: request/response contracts; don’t expose entities over HTTP.

Services/Interfaces/: service contracts (IUserService, IPostService).

Services/Implementations/: in-memory implementations (lists/dictionaries).

Endpoints/: extension methods that map routes; keeps Program.cs clean.

## Data Model (minimal)
User: Id (Guid), Name (string), Email (string), CreatedAt (DateTimeOffset)

Post: Id (Guid), UserId (Guid), Title (string), Content (string), PublishedAt (DateTimeOffset?)
(You may add fields if helpful.)

## DTOs (design suggestions)
## Users

CreateUserDto { string Name, string Email }

UpdateUserDto { string? Name, string? Email } (partial updates allowed)

UserResponseDto { Guid Id, string Name, string Email, DateTimeOffset CreatedAt }

## Posts

CreatePostDto { Guid UserId, string Title, string Content }

UpdatePostDto { string? Title, string? Content }

PostResponseDto { Guid Id, Guid UserId, string Title, string Content, DateTimeOffset? PublishedAt }

## Services (interfaces)
```
// Services/Interfaces/IUserService.cs
public interface IUserService {
  Task<User?> GetAsync(Guid id);
  Task<IReadOnlyList<User>> ListAsync();
  Task<User> CreateAsync(string name, string email);
  Task<User?> UpdateAsync(Guid id, string? name, string? email);
  Task<bool> DeleteAsync(Guid id);
}

// Services/Interfaces/IPostService.cs
public interface IPostService {
  Task<Post?> GetAsync(Guid id);
  Task<IReadOnlyList<Post>> ListAsync();
  Task<IReadOnlyList<Post>> ListByUserAsync(Guid userId);
  Task<Post> CreateAsync(Guid userId, string title, string content);
  Task<Post?> UpdateAsync(Guid id, string? title, string? content);
  Task<bool> DeleteAsync(Guid id);
}
```

### Implementation notes

Services should use List<T>/Dictionary<Guid,T> for storage.

Creating a post requires an existing UserId.

Deleting a user should either cascade delete posts or fail if posts exist—choose and document the behavior.

Register services in Program.cs as singletons (to only have one instance and share the in-memory data)

## Endpoint Requirements
### Users

GET /users → 200 OK with UserResponseDto[]

GET /users/{id:guid} → 200 OK with UserResponseDto or 404

POST /users (CreateUserDto) → 201 Created with UserResponseDto + Location

PATCH /users/{id:guid} (UpdateUserDto) → 200 OK with updated UserResponseDto or 404

DELETE /users/{id:guid} → 204 No Content or 404

GET /users/{id:guid}/posts → 200 OK with PostResponseDto[]

## Posts

GET /posts → 200 OK with PostResponseDto[]

GET /posts/{id:guid} → 200 OK with PostResponseDto or 404

POST /posts (CreatePostDto) → 201 Created with PostResponseDto + Location (validate UserId exists → else 400)

PATCH /posts/{id:guid} (UpdatePostDto) → 200 OK with updated PostResponseDto or 404

DELETE /posts/{id:guid} → 204 No Content or 404

## Hints & Constraints
Prefer DTOs at the HTTP boundary; map to/from models.

Use Results.* helpers: Ok, Created, NoContent, NotFound, BadRequest.

For Location on 201, point to the created resource URL.

Keep Program.cs minimal—wiring only.
