using Microsoft.Extensions.Configuration;
using static System.ArgumentException;
using static Heimdall.Abstractions.Constants;

namespace Heimdall.Abstractions.Extensions;

public static class ConfigurationExtensions
{
    public static string GetServiceName(this IConfiguration configuration)
    {
        var serviceName = configuration[ServiceNameTagKey];
        ThrowIfNullOrWhiteSpace(serviceName);

        return serviceName;
    }

    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        var connectionString = configuration.GetConnectionString(name);
        ThrowIfNullOrWhiteSpace(connectionString, $"ConnectionStrings:{name}");

        return connectionString;
    }
}
