namespace Heimdall.Common.Abstraction;

public interface ILastUpdated : ICreated
{
    public DateTime LastUpdatedAt { get; set; }
    
    public string LastUpdatedBy { get; set; }
}
