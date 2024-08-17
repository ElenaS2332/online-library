using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface ISubscriptionsRepository
{
    IEnumerable<Subscription> GetAllSubscriptions();
    Subscription? GetDetailsForSubscription(Guid id);
}