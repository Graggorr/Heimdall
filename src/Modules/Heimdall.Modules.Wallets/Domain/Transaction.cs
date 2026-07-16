namespace Heimdall.Modules.Wallets.Domain;

public sealed record Transaction
{
    public Guid Id { get; init; }

    public Guid WalletId { get; init; }

    public long Amount { get; init; }

    public DateTime Date { get; init; }

    public string? Description { get; init; }
}
