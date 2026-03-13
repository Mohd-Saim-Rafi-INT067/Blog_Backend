using BlogApp.Models;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;

namespace BlogApp.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(UserRole? role = null);
    Task<UserResponseDto> GetUserByIdAsync(int id);
    Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto dto, int requesterId, string requesterRole);
    Task DeleteUserAsync(int id, int requesterId, string requesterRole);
}

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

public interface ITopicService
{
    Task<IEnumerable<TopicResponseDto>> GetAllTopicsAsync();
    Task<TopicWithBlogsDto> GetTopicByIdAsync(int id);
    Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto dto, int authorId);
    Task DeleteTopicAsync(int id, string requesterRole);
}

public interface ICommentService
{
    Task<IEnumerable<CommentResponseDto>> GetCommentsByBlogIdAsync(int blogId);
    Task<CommentResponseDto> CreateCommentAsync(int blogId, CreateCommentDto dto, int authorId);
    Task<CommentResponseDto> UpdateCommentAsync(int blogId,int commentId ,UpdateCommentDto dto, int userId, string role);
    Task DeleteCommentAsync(int blogId, int commentId, int userId, string role);
}

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionResponseDto>> GetMySubscriptionsAsync(int userId);
    Task<IEnumerable<SubscriberResponseDto>> GetAuthorSubscribersAsync(int authorId);
    Task SubscribeAsync(int subscriberId, int authorId);
    Task UnsubscribeAsync(int subscriberId, int authorId);
}