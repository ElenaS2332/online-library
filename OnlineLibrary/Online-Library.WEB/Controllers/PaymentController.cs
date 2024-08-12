using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Domain;
using Online_Library.Domain.Entities;
using Online_Library.Domain.Enums;
using Stripe;

namespace Online_Library.WEB.Controllers;

public class PaymentController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public PaymentController(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public IActionResult Index(string userId, SubscriptionType subscriptionType)
    {
        StripeConfiguration.ApiKey = "sk_test_51Io84IHBiOcGzrvu4sxX66rTHq8r5nxIxRiJPbOHB4NwVJOE1jSlxgYe741ITs024uXhtpBFtxm3RoCZc3kafocC00IhvgxkL0";
    
        var amount = subscriptionType == SubscriptionType.Yearly ? 60 : 8; 

        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                { "UserId", userId },
                { "SubscriptionType", subscriptionType == SubscriptionType.Yearly ? "Yearly" : "Monthly" }
            }
        });

        var model = new PaymentViewModel
        {
            UserId = userId,
            Amount = amount,
            SubscriptionType = subscriptionType,
            PublishableKey = "pk_test_51Io84IHBiOcGzrvuW2PMQh3Jy4yF1CmDCvIrYGgAhoo2qolU9KLvEh5RalmoqL0Yji0FMAt5XBEU6l8Tn4pMSI5e007fOezyoC",
            ClientSecret = paymentIntent.ClientSecret
        };

        return View(model);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreatePaymentIntent(string userId, SubscriptionType subscriptionType)
    {
        var amount = subscriptionType == SubscriptionType.Yearly ? 60 : 8;

        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            Metadata = new Dictionary<string, string>
            {
                { "UserId", userId },
                { "SubscriptionType", subscriptionType == SubscriptionType.Yearly ? "Yearly" : "Monthly" }
            }
        });

        ViewBag.ClientSecret = paymentIntent.ClientSecret;

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }


    public async Task<IActionResult> PaymentSuccess(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return View("Success");
        user.EmailConfirmed = true; 
        await _userManager.UpdateAsync(user);

        return View("Success");
    }
}
