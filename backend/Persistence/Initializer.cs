using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Persistence.Categories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpenseTracker.Persistence;

public static class Initializer
{
    public static void InitDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext>(options =>
            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

        var repositoryClasses = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => !x.IsInterface && !x.IsAbstract && x.IsClass)
            .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRepository<>)))
            .ToArray();

        // Register extra repository
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        foreach (var r in repositoryClasses)
        {
            foreach (var i in r.GetInterfaces())
            {
                services.AddScoped(i, r);
            }
        }
    }

    public static void UseEntityFramework(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<DbContext>()
            ?? throw new InvalidOperationException($"Cantextes where not injected with: {nameof(InitDatabase)}");

        dbContext.Database.EnsureCreated();
    }
}
