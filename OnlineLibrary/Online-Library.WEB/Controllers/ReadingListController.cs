using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.Controllers;

[Authorize]
public class ReadingListController(
    IReadingListService readingListService, 
    UserManager<User> userManager) : Controller
{
    // GET
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return NotFound();
        }

        var readingList = readingListService.GetReadingListInfo(userId);
        
        return View(readingList);
    }
    
    [HttpPost]
    public IActionResult ClearReadingList()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return NotFound();
        }

        readingListService.RemoveAllBooksFromReadingList(userId);
        return RedirectToAction("Index");
    }
}