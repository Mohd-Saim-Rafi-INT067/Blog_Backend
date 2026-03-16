using BlogApp.DTOs.Blogs;
namespace BlogApp.DTOs.Topics;
using System.ComponentModel.DataAnnotations;
public class CreateTopicDto
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = null!;
}

public class TopicResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AuthorId { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class TopicWithBlogsDto : TopicResponseDto
{
    public List<BlogResponseDto> Blogs { get; set; } = new();
}

