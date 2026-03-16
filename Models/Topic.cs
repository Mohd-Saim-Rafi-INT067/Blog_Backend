namespace BlogApp.Models;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Author { get; set; } = null!;
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}