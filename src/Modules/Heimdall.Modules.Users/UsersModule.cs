using Heimdall.Data.Extensions;
using Heimdall.Modules.Users.Contracts;
using Heimdall.Modules.Users.Data;
using Heimdall.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heimdall.Modules.Users;

public sealed class UsersModule(IConfiguration configuration) : BaseModule(configuration)
{
    protected override void Load(IServiceCollection services)
    {
        base.Load(services);

        services.AddModuleDbContext<UsersDbContext>(_configuration, UsersDbContext.Schema);
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
