namespace OnlineLibraryAdmin.Models;

public class Subscription
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public bool IsPaid { get; set; }
}