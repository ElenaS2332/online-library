namespace Online_Library.Domain.Entities;

public class MonthlySubscription : Subscription
{
    public override string GetSubscriptionDetails()
    {
        return $"Monthly Subscription: {Price:C} per month";
    }
}