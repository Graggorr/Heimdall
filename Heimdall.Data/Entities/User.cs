using Heimdall.Data.Common;

namespace Heimdall.Data.Entities;

public sealed record User : IEntity
{
    public Guid Id { get; init; }
    
    public string HashedPassword { get; init; }
    
    public string Salt { get; init; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CreatedBy { get; set; }
    
    public DateTime LastUpdatedAt { get; set; }
    
    public string LastUpdatedBy { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public UserProfile UserProfile { get; init; }
    
    public Address Address { get; init; }

    public List<Wallet> Wallets { get; init; } = [];
}
