using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.Services;

namespace UOC.Consent.Platform.Infrastructure;

public static class DependencyInjection
{
    private const string DefaultConnection = "DefaultConnection";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        /*
        var connectionString = configuration.GetConnectionString(DefaultConnection);
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        */
        var connectionString = configuration.GetConnectionString(DefaultConnection);
        services
            .AddScoped<LedgerService>()
            .AddDbContextFactory<ApplicationDbContext>(options =>
                options
                    .LogTo(query => System.Diagnostics.Debug.WriteLine(query))
                    .UseSqlServer(
                        connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

        return services;
    }
}