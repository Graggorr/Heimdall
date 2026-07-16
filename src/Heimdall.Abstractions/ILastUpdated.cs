namespace Heimdall.Abstractions;

public interface ILastUpdated
{
    public DateTime LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; }
}
