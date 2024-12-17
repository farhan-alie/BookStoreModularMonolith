using System.Reflection;
using BookStore.Books;
using BookStore.Users;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(s =>
        s.SigningKey = builder.Configuration.GetValue<string>("Auth:JwtSecret")!)
    .AddAuthorization()
    .SwaggerDocument();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookServices(builder.Configuration, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, mediatRAssemblies);

// Set up MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen();

await app.RunAsync();

#pragma warning disable CA1515
namespace BookStore.Web
{
    public abstract class Program;
}
