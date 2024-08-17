using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class SubscriptionsService(ISubscriptionsRepository subscriptionsRepository) : ISubscriptionsService
{
    public IEnumerable<Subscription> GetAllSubscriptions()
    {
        return subscriptionsRepository.GetAllSubscriptions();
    }

    public Subscription? GetDetailsForSubscription(Guid id)
    {
        return subscriptionsRepository.GetDetailsForSubscription(id);
    }
}