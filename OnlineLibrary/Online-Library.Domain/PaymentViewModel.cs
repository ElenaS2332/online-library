using Online_Library.Domain.Enums;

namespace Online_Library.Domain;

public class PaymentViewModel
{
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
    public string PublishableKey { get; set; }
    public string ClientSecret { get; set; }
}

