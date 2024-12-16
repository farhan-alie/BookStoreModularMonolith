using System.Reflection;
using BookStore.Books;
using BookStore.Users;
using FastEndpoints;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, mediatRAssemblies);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseFastEndpoints();

await app.RunAsync();

#pragma warning disable CA1515
namespace BookStore.Web
{
    public abstract class Program;
}
