namespace BlogApp.DTOs.Blogs;
using System.ComponentModel.DataAnnotations;
public class CreateBlogDto
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
    [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
    public string Title {get; set; } = null!;

    [Required(ErrorMessage = "Content is required")]
    [MinLength(10, ErrorMessage = "Content must be at least 10 characters long")]
    public string Content { get; set; } = null!;

    [Url(ErrorMessage = "Invalid URL format")]
    public string? BannerImage { get; set; }
    public bool IsPublished { get; set; } = false;

    [Required(ErrorMessage = "TopicId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Valid TopicId is required")]
    public int TopicId { get; set; }
    [MaxLength(255, ErrorMessage = "Slug cannot exceed 255 characters")]
    public string? Slug { get; set; }
}

public class UpdateBlogDto
{
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
    [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
    public string? Title {get; set; }
    [MinLength(10, ErrorMessage = "Content must be at least 10 characters long")]
    public string? Content { get; set; }
    [Url(ErrorMessage = "Invalid URL format")]
    public string? BannerImage { get; set; }
    public bool? IsPublished { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Valid TopicId is required")]
    public int? TopicId { get; set; }
    [MaxLength(255, ErrorMessage = "Slug cannot exceed 255 characters")]
    public string? Slug { get; set; }
}

public class PublishBlogDto
{
    [Required(ErrorMessage = "IsPublished is required.")]
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
