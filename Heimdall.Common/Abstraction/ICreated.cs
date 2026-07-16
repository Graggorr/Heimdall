namespace Heimdall.Common.Abstraction;

public interface ICreated
{
    public DateTime CreatedAt { get; set; }
    
    public string CreatedBy { get; set; }
}
