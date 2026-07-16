namespace Heimdall.Data;

public sealed record Address
{
    public string MainAddress { get; set; }
    
    public string SecondaryAddress { get; set; }
    
    public string PostalCode { get; set; }
    
    public string CountryCode { get; set; }
    
    public string City { get; set; }
}
