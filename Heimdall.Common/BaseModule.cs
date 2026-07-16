using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Mapster.TypeAdapterConfig;
using Module = ServiceCollection.Extensions.Modules.Module;

namespace Heimdall.Common;

public abstract class BaseModule : Module
{
    protected readonly Assembly _assembly;

    protected readonly IConfiguration _configuration;

    protected BaseModule(IConfiguration configuration)
    {
        _assembly = GetType().Assembly;
        _configuration = configuration;
    }

    protected override void Load(IServiceCollection services)
    {
        GlobalSettings.Scan(_assembly);
    }
}
