using System.Reflection;
using BookStore.Users.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Users;

public static class UsersModuleExtensions
{
    public static IServiceCollection AddUserModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("Users");
        services.AddDbContext<UsersDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();

        // Add User Services
        services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();

        // if using MediatR in this module, add any assemblies that contain handlers to the list
        mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);

        return services;
    }
}
