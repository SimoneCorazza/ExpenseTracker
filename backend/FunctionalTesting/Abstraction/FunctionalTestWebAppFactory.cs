using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Minio;
using Testcontainers.Minio;
using Testcontainers.PostgreSql;

namespace ExpenseTracker.FunctionalTesting.Abstraction;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:18-alpine")
        .WithDatabase("expense_tracker_test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly MinioContainer minioContainer = new MinioBuilder()
      .WithImage("minio/minio:latest")
      .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<Persistence.DbContext>>();
            services.AddDbContext<Persistence.DbContext>(options =>
            {
                // Make sure is the same configuration as the original
                options
                    .UseNpgsql(dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention();
            });

            var u = new Uri(minioContainer.GetConnectionString());
            string endpoint = $"{u.Host}:{u.Port}";

            services.RemoveAll<IMinioClient>();
            services.AddMinio(x => x
                .WithSSL(false) // Added for test only
                .WithEndpoint(endpoint)
                .WithCredentials(minioContainer.GetAccessKey(), minioContainer.GetSecretKey())
                .Build());
        });
    }

    public async Task InitializeAsync()
    {
        await dbContainer.StartAsync();
        await minioContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await base.DisposeAsync();
        await dbContainer.StopAsync();
        await minioContainer.StopAsync();
        await minioContainer.StopAsync();
    }
}
