namespace Heimdall.Abstractions;

public interface IEntity : ICreated, ILastUpdated, IDeleted
{
    public Guid Id { get; init; }
}
