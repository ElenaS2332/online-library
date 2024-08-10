using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.Controllers;

public class ReadingListController(
    IReadingListService readingListService, 
    UserManager<User> userManager) : Controller
{
    // GET
    public IActionResult Index()
    {
        var userId = User.Identity?.Name ?? "";
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var readingList = readingListService.GetReadingListInfo(userId);
        
        return View(readingList);
    }
}