namespace BlogApp.Models;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? BannerImage { get; set; }
    public string Slug { get; set; } = null!;
    public bool IsPublished { get; set; } = false;
    public DateTime? PublishedAt{ get; set; }
    public int AuthorId { get; set; }
    public int TopicId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User Author { get; set; } = null!;
    public Topic Topic { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}