using Heimdall.Common.Abstraction;

namespace Heimdall.Data.Common;

public interface IEntity : ICreated, ILastUpdated, IDeleted
{
    public Guid Id { get; init; }
}
