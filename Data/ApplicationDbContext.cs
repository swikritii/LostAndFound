using LostAndFound.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemClaim> ItemClaims { get; set; }
    }
}