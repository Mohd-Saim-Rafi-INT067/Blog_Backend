using BlogApp.Models;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;

namespace BlogApp.Services.Interfaces;


public interface IBlogService
{
    Task<IEnumerable<BlogResponseDto>> GetAllPublishedBlogsAsync(string? title = null);
    Task<BlogResponseDto> GetBlogByIdAsync(int id);
    Task<BlogResponseDto> GetBlogBySlugAsync(string slug);
    Task<IEnumerable<BlogResponseDto>> GetBlogsByAuthorAsync(int authorId, bool? isPublished= null);
    Task<BlogResponseDto> CreateBlogAsync(CreateBlogDto dto, int authorId);
    Task<BlogResponseDto> UpdateBlogAsync(int id, UpdateBlogDto dto, int requesterId, string requesterRole);
    Task<BlogResponseDto> PublishBlogAsync(int id, bool isPublished, int requesterId, string requesterRole);
    Task DeleteBlogAsync(int id, int requesterId, string requesterRole);
}