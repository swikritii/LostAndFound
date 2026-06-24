namespace LostAndFound.Models.Entities
{
    public class ItemClaim
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ClaimedAt { get; set; } = DateTime.UtcNow;

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

