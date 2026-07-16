using Heimdall.Modules.Wallets.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heimdall.Modules.Wallets.Data.Configurations;

internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(wallet => wallet.Id);
        builder.Property(wallet => wallet.CreatedBy).HasMaxLength(256);
        builder.Property(wallet => wallet.LastUpdatedBy).HasMaxLength(256);
        builder.HasIndex(wallet => wallet.UserId);
        builder.HasQueryFilter(wallet => !wallet.IsDeleted);

        builder.HasMany(wallet => wallet.Transactions)
            .WithOne()
            .HasForeignKey(transaction => transaction.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
