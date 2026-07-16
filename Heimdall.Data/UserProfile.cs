namespace Heimdall.Data;

public record UserProfile
{
    public required string UserName { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string Email { get; set; }
    
    public string PassportNumber { get; init; }
}
