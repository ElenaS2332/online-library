namespace OnlineLibraryAdmin.Models;

public class User 
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public Guid UserSubscriptionId { get; set; }
}