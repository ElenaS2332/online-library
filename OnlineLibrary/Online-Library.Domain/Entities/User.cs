using System.ComponentModel.DataAnnotations;

namespace Online_Library.Domain.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }
    
    public Guid? UserSubscriptionId { get; set; }
    public Subscription? UserSubscription { get; set; }
    
    public Guid? ReadingListId { get; set; }

    public ReadingList? ReadingList { get; set; }
}