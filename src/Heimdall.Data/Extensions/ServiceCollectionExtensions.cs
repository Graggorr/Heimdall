using Heimdall.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Heimdall.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModuleDbContext<TContext>(this IServiceCollection services, IConfiguration configuration, string schema)
        where TContext : DbContext
    {
        services.TryAddSingleton(TimeProvider.System);
        services.TryAddSingleton<AuditingSaveChangesInterceptor>();

        return services.AddDbContext<TContext>((provider, options) => options
            .UseNpgsql(
                configuration.GetRequiredConnectionString(nameof(Heimdall)),
                npgsql => npgsql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schema))
            .AddInterceptors(provider.GetRequiredService<AuditingSaveChangesInterceptor>()));
    }
}
