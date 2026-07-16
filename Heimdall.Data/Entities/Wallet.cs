using Heimdall.Data.Common;

namespace Heimdall.Data.Entities;

public sealed record Wallet
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public Currency Currency { get; init; }
    
    public int Balance { get; init; }
    
    public bool IsDeleted { get; set; }
    
    public bool IsClosed { get; set; }
    
    public required User User { get; init; }

    public List<Transaction> Transactions { get; init; } = [];
}
