using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Data;

namespace LostAndFound.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var items = await dbContext.Items
    .Include(i => i.Category)
    .OrderByDescending(i => i.ReportedAt)
    .ToListAsync();

            return View(items);
        }
    }
}