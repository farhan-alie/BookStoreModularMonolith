using System.Reflection;
using BookStore.Books.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Books;

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services,
        ConfigurationManager configuration,
        List<Assembly> mediatRAssemblies)
    {
        string connectionString = configuration.GetConnectionString("Books")!;
        services.AddDbContext<BooksDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IBookRepository, EfBookRepository>();
        services.AddScoped<IBookService, BookService>();

        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(BookServiceExtensions).Assembly);
        return services;
    }
}
