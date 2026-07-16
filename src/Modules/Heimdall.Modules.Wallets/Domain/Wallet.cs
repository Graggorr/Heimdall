using Heimdall.Abstractions;

namespace Heimdall.Modules.Wallets.Domain;

public sealed record Wallet : IEntity
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Currency Currency { get; init; }

    public long Balance { get; set; }

    public bool IsClosed { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public List<Transaction> Transactions { get; init; } = [];
}
