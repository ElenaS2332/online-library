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
    
    [Required]
    public Guid UserSubscriptionId { get; set; }
    
    [Required]
    public Subscription UserSubscription { get; set; }
    
    [Required]
    public Guid ReadingListId { get; set; }

    [Required]
    public ReadingList ReadingList { get; set; }
}