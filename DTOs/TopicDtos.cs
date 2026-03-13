using BlogApp.DTOs.Blogs;
namespace BlogApp.DTOs.Topics;

public class CreateTopicDto
{
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

