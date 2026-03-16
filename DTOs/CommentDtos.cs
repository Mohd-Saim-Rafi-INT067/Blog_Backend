namespace BlogApp.DTOs.Comments;
using System.ComponentModel.DataAnnotations;
public class CreateCommentDto
{
    [Required(ErrorMessage = "Content is required")]
    [MinLength(1, ErrorMessage = "Content must be at least 1 character long")]
    [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters")]
    public string Content { get; set; } = null!;
}
public class UpdateCommentDto
{
    [Required(ErrorMessage = "Content is required")]
    [MinLength(1, ErrorMessage = "Content must be at least 1 characters long")]
    [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters")]
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