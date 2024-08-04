using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using System.Threading.Tasks;
using Online_Library.Repository;

namespace Online_Library.WEB.Controllers
{
    [Authorize]
    public class ProfileController(UserManager<User> userManager, ApplicationDbContext dbContext)
        : Controller
    {
        private readonly UserManager<User> _userManager = userManager;

        // GET: /Profile/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name ?? "";
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await dbContext.Users
                .Include(u => u.UserSubscription)
                .FirstOrDefaultAsync(u => u.Email == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return View(user);
        }
    }
}