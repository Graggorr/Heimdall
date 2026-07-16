using Heimdall.Modules.Wallets.Domain;
using Microsoft.EntityFrameworkCore;

namespace Heimdall.Modules.Wallets.Data;

internal sealed class WalletsDbContext(DbContextOptions<WalletsDbContext> options) : DbContext(options)
{
    public const string Schema = "wallets";

    public DbSet<Wallet> Wallets => Set<Wallet>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
