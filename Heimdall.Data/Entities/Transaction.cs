namespace Heimdall.Data.Entities;

public sealed record Transaction
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid WalletId { get; init; }
    
    public DateTime Date { get; init; }
    
    public string Description { get; init; }
    
    public required User User { get; init; }
    
    public required Wallet Wallet { get; init; }
}
