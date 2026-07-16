namespace Heimdall.Abstractions;

public interface ICreated
{
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }
}
