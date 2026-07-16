namespace Heimdall.Modules.Users.Domain;

public sealed record Address
{
    public required string MainAddress { get; set; }

    public string? SecondaryAddress { get; set; }

    public required string PostalCode { get; set; }

    public required string CountryCode { get; set; }

    public required string City { get; set; }
}
