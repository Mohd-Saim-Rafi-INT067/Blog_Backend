namespace BlogApp.DTOs.Comments;

public class CreateCommentDto
{
    public string Content { get; set; } = null!;
}
public class UpdateCommentDto
{
    public string Content { get; set; } = null!;
}

public class CommentResponseDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
    public string Username {get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}