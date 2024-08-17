using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Domain;
using Online_Library.Domain.Entities;
using Online_Library.Domain.Enums;
using Online_Library.Service.Interfaces;
using Stripe;

namespace Online_Library.WEB.Controllers;

public class PaymentController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUsersService _usersService;
    private readonly UserManager<User> _userManager;

    public PaymentController(IConfiguration configuration, UserManager<User> userManager, IUsersService usersService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _usersService = usersService;
    }

    public IActionResult Index(string userId, string subscriptionType)
    {
        StripeConfiguration.ApiKey = "sk_test_51Io84IHBiOcGzrvu4sxX66rTHq8r5nxIxRiJPbOHB4NwVJOE1jSlxgYe741ITs024uXhtpBFtxm3RoCZc3kafocC00IhvgxkL0";

        var user = _userManager.FindByIdAsync(userId).Result;
        if (user == null || !user.EmailConfirmed)
        {
            return NotFound("User not found or email not confirmed.");
        }

        var amount = subscriptionType == "Yearly" ? 6000 : 800; 
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                { "UserId", userId },
                { "SubscriptionType", subscriptionType == "Yearly" ? "Yearly" : "Monthly" }
            }
        });

        var model = new PaymentViewModel
        {
            UserId = userId,
            Amount = amount,
            SubscriptionType =  subscriptionType == "Yearly" ? SubscriptionType.Yearly : SubscriptionType.Monthly,
            PublishableKey = "pk_test_51Io84IHBiOcGzrvuW2PMQh3Jy4yF1CmDCvIrYGgAhoo2qolU9KLvEh5RalmoqL0Yji0FMAt5XBEU6l8Tn4pMSI5e007fOezyoC",
            ClientSecret = paymentIntent.ClientSecret
        };

        return View(model);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreatePaymentIntent(string userId, string subscriptionType)
    {
        var amount = subscriptionType == "Yearly" ? 6000 : 800; 

        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                { "UserId", userId },
                { "SubscriptionType", subscriptionType == "Yearly" ? "Yearly" : "Monthly" }
            }
        });

        ViewBag.ClientSecret = paymentIntent.ClientSecret;

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }
    
    public async Task<IActionResult> PaymentSuccess(string userId, string email, string returnUrl = null)
    {
        // var user = await _userManager.FindByIdAsync(userId);
        var user = _usersService.GetUser(userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.UserSubscription.IsPaid = true;
        await _userManager.UpdateAsync(user);

        return View("Success"); // Ensure you have a view named "Success"
    }

}
