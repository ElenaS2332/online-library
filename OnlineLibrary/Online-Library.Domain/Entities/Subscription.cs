namespace Online_Library.Domain.Entities;

public abstract class Subscription
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public abstract string GetSubscriptionDetails();
}