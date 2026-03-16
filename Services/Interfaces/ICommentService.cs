using BlogApp.Models;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;

namespace BlogApp.Services.Interfaces;


public interface ICommentService
{
    Task<IEnumerable<CommentResponseDto>> GetCommentsByBlogIdAsync(int blogId);
    Task<CommentResponseDto> CreateCommentAsync(int blogId, CreateCommentDto dto, int authorId);
    Task<CommentResponseDto> UpdateCommentAsync(int blogId,int commentId ,UpdateCommentDto dto, int userId, string role);
    Task DeleteCommentAsync(int blogId, int commentId, int userId, string role);
}
