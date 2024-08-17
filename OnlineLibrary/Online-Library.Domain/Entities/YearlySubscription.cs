namespace Online_Library.Domain.Entities;

public class YearlySubscription : Subscription
{
    public override string GetSubscriptionDetails()
    {
        return $"Yearly Subscription: {Price} USD per year";
    }
}