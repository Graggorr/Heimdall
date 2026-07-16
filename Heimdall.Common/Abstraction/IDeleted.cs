namespace Heimdall.Common.Abstraction;

public interface IDeleted : ILastUpdated
{
    public bool IsDeleted { get; set; }
}
