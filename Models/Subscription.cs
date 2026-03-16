namespace BlogApp.Models;

public class Subscription
{
    public int SubscriberId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Subscriber { get; set; } = null!;
    public User Author { get; set; } = null!;
}