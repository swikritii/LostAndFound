using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Data;
using LostAndFound.Models;
using LostAndFound.Models.Entities;
using static LostAndFound.Models.AddItemViewModel;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ItemsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /Items/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var item = await dbContext.Items
                .Include(i => i.Category)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var viewModel = new AddItemViewModel
            {
                Categories = await dbContext.Categories
                    .Select(c => new AddItemViewModel.CategoryOption { Id = c.Id, Name = c.Name })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddItemViewModel viewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                viewModel.Categories = await dbContext.Categories
                    .Select(c => new AddItemViewModel.CategoryOption { Id = c.Id, Name = c.Name })
                    .ToListAsync();
                return View(viewModel);
            }

            var item = new Item
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Status = viewModel.Status,
                CategoryId = viewModel.CategoryId,
                ReportedAt = DateTime.UtcNow,
                UserId = userId.Value
            };

            await dbContext.Items.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
