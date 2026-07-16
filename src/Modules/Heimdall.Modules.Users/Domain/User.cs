using Heimdall.Abstractions;

namespace Heimdall.Modules.Users.Domain;

public sealed record User : IEntity
{
    public Guid Id { get; init; }

    public required string HashedPassword { get; init; }

    public required string Salt { get; init; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public required UserProfile Profile { get; init; }

    public required Address Address { get; init; }
}
