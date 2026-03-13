namespace BlogApp.DTOs.Blogs;

public class CreateBlogDto
{
    public string Title {get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? BannerImage { get; set; }
    public bool IsPublished { get; set; } = false;
    public int TopicId { get; set; }
    public string? Slug { get; set; }
}

public class UpdateBlogDto
{
    public string? Title {get; set; }
    public string? Content { get; set; }
    public string? BannerImage { get; set; }
    public bool? IsPublished { get; set; }
    public int? TopicId { get; set; }
    public string? Slug { get; set; }
}

public class PublishBlogDto
{
    public bool IsPublished { get; set; }
}

public class BlogResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? BannerImage { get; set; }
    public string Slug { get; set; } = null!;
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public AuthorDto Author { get; set; } = null!;
    public TopicDto Topic { get; set; } = null!;
}

public class AuthorDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
}

public class TopicDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
