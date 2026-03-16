using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repo;
    public CommentService(ICommentRepository repo)
    {
        _repo = repo;
    }
    public async Task<IEnumerable<CommentResponseDto>> GetCommentsByBlogIdAsync(int blogId)
    {
        var comments = await _repo.GetByBlogIdAsync(blogId);
        return comments.Select(MapToDto);
    }
    public async Task<CommentResponseDto> CreateCommentAsync(int blogId, CreateCommentDto dto, int authorId)
    {
        var comment = new Comment{ Content = dto.Content, BlogId = blogId, UserId = authorId };
        await _repo.CreateAsync(comment);
        var created = await _repo.GetByIdAsync(comment.Id) ?? throw new Exception("Failed to retrieve created comment.");
        return MapToDto(created);
    }
    public async Task<CommentResponseDto> UpdateCommentAsync(int blogId, int commentId, UpdateCommentDto dto, int userId, string role)
    {
        var comment = await _repo.GetByIdAsync(commentId) ?? throw new KeyNotFoundException("Comment not found.");
        if (comment.BlogId != blogId) throw new KeyNotFoundException("Comment not found on this post.");
        if (comment.UserId != userId && role != "Admin") throw new UnauthorizedAccessException("Access denied");
        comment.Content = dto.Content;
        await _repo.UpdateAsync(comment);
        return MapToDto(comment);
    }
    public async Task DeleteCommentAsync(int blogId, int commentId, int userId, string role)
    {
        var comment = await _repo.GetByIdAsync(commentId) ?? throw new KeyNotFoundException("Comment not found.");
        if (comment.BlogId != blogId) throw new KeyNotFoundException("Comment not found on this post.");
        if (comment.UserId != userId && role != "Admin")
            throw new UnauthorizedAccessException("Access denied.");
        await _repo.DeleteAsync(comment);
    }
    private static CommentResponseDto MapToDto(Comment c) => new()
    {
        Id = c.Id, Content = c.Content, UserId = c.UserId,
        Username = c.User?.Username ?? "", CreatedAt = c.CreatedAt, UpdatedAt = c.UpdatedAt
    };
}