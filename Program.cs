
using Scalar.AspNetCore;
using BlogApi.Endpoints;
using BlogApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IPostService, InMemoryPostService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapUserEndpoints();
app.MapPostEndpoints();

app.Run();