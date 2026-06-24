namespace LostAndFound.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }      
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}