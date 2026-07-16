using Heimdall.Data.Extensions;
using Heimdall.Modules.Wallets.Contracts;
using Heimdall.Modules.Wallets.Data;
using Heimdall.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heimdall.Modules.Wallets;

public sealed class WalletsModule(IConfiguration configuration) : BaseModule(configuration)
{
    protected override void Load(IServiceCollection services)
    {
        base.Load(services);

        services.AddModuleDbContext<WalletsDbContext>(_configuration, WalletsDbContext.Schema);
        services.AddScoped<IWalletRepository, WalletRepository>();
    }
}
