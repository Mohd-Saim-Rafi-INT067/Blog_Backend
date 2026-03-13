namespace BlogApp.Models;

public enum UserRole {User, Author, Admin}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
}