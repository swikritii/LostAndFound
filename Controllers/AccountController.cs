using LostAndFound.Data;
using LostAndFound.Models.Entities;
using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existing = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existing != null)
                {
                    ModelState.AddModelError("Email", "Email already registered.");
                    return View(model);
                }

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = model.Password, // Hash this in production!
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Store user info in session
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetInt32("UserId", user.Id);

                //  Redirect to Dashboard after register
                return RedirectToAction("Index", "Dashboard");
            }

            return View(model);
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email
                                           && u.PasswordHash == model.Password);

                if (user != null)
                {
                    // Store user info in session
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    //  Redirect to Dashboard after login
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}