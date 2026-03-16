namespace BlogApp.DTOs.Subscriptions;

public class SubscriptionResponseDto
{
    public int AuthorId { get; set; }
    public string Username { get; set; } = null!;
    public DateTime SubscribedSince { get; set; }
}
public class SubscriberResponseDto
{
    public int SubscriberId { get; set; }
    public string Username { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}