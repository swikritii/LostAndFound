using LostAndFound.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Redirect to login if not logged in
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            // Pass stats to the view via ViewBag
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.TotalItems = await _context.Items.CountAsync();
            ViewBag.LostItems = await _context.Items.CountAsync(i => i.Status == "Lost");
            ViewBag.FoundItems = await _context.Items.CountAsync(i => i.Status == "Found");
            ViewBag.PendingClaims = await _context.ItemClaims.CountAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync();

            return View();
        }
    }
}