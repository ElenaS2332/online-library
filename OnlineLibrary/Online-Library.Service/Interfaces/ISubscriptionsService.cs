using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface ISubscriptionsService
{
    IEnumerable<Subscription> GetAllSubscriptions();
    Subscription? GetDetailsForSubscription(Guid id);
}