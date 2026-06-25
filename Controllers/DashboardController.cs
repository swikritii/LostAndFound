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

        public async Task<IActionResult> Index(string? filter)
        {
            // Redirect to login if not logged in
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            // ── Stats (always full count, not filtered) ──
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.TotalItems = await _context.Items.CountAsync();
            ViewBag.LostItems = await _context.Items.CountAsync(i => i.Status == "Lost");
            ViewBag.FoundItems = await _context.Items.CountAsync(i => i.Status == "Found");
            ViewBag.PendingClaims = await _context.ItemClaims.CountAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            ViewBag.CurrentFilter = filter ?? "all";

            // ── Items (filtered) ──
            var query = _context.Items
                .Include(i => i.Category)   // ← Category load garcha
                .Include(i => i.User)       // ← User load garcha
                .OrderByDescending(i => i.ReportedAt)
                .AsQueryable();

            if (filter == "lost")
                query = query.Where(i => i.Status == "Lost");
            else if (filter == "found")
                query = query.Where(i => i.Status == "Found");

            var items = await query.ToListAsync();

            return View(items);
        }
    }
}
