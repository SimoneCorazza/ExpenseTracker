using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Users;
using System.Reflection;

namespace Persistence
{
    public static class Initializer
    {
        public static void InitDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionString));

            var repositoryClasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.GetCustomAttributes().Any(attr => attr.GetType().Equals(typeof(RepositoryAttribute))));

            foreach (var r in repositoryClasses)
            {
                var @interface = r.GetInterfaces().First();
                services.AddScoped(@interface, r);
            }
        }

        public static void UseEntityFramework(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<UsersDbContext>()
                ?? throw new InvalidOperationException($"Cantextes where not injected with: {nameof(InitDatabase)}");

            dbContext.Database.EnsureCreated();
        }
    }
}
