namespace Heimdall.Modules.Users.Domain;

public sealed record UserProfile
{
    public required string UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public required string Email { get; set; }

    public string? PassportNumber { get; init; }
}
