using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class SubscriptionsRepository(ApplicationDbContext context) : ISubscriptionsRepository
{
    public IEnumerable<Subscription> GetAllSubscriptions()
    {
        return context.Subscriptions
            .Include(s => s.User)
            .ToList();
    }

    public Subscription? GetDetailsForSubscription(Guid id)
    {
        return context.Subscriptions
            .Include(s => s.User)
            .FirstOrDefault(s => s.Id == id);
    }
}