using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        ViewBag.PublishableKey = "pk_test_51Io84IHBiOcGzrvuW2PMQh3Jy4yF1CmDCvIrYGgAhoo2qolU9KLvEh5RalmoqL0Yji0FMAt5XBEU6l8Tn4pMSI5e007fOezyoC";
        ViewBag.Amount = subscriptionType == SubscriptionType.Yearly ? 999 : 179; // Example amounts
        ViewBag.SubscriptionType = subscriptionType;
        ViewBag.UserId = userId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaymentIntent(string userId, SubscriptionType subscriptionType)
    {
        var amount = subscriptionType == SubscriptionType.Yearly ? 999 : 179; // Example amounts in mkd

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

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }

    public async Task<IActionResult> PaymentSuccess(string userId)
    {
        // Retrieve the user and update their subscription status
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            // Update user status, e.g., activate the account
            user.EmailConfirmed = true; // Example: setting email as confirmed
            await _userManager.UpdateAsync(user);
        }

        return View("Success");
    }
}
