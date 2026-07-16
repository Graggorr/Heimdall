using Microsoft.Extensions.Configuration;
using static System.ArgumentException;
using static Heimdall.Common.Constants;

namespace Heimdall.Common.Extensions;

public static class ConfigurationExtensions
{
    public static string GetServiceName(this IConfiguration configuration)
    {
        var serviceName = configuration[ServiceNameTagKey];
        ThrowIfNullOrWhiteSpace(serviceName);
        
        return serviceName;
    }
}
