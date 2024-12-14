using BookStore.Web;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
string[] summaries =
[
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
];

app.MapGet("/weatherforecast", () =>
{
    WeatherForecast[] forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
           DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
#pragma warning disable CA5394
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
#pragma warning restore CA5394
        ))
        .ToArray();
    return forecast;
});

await app.RunAsync();
