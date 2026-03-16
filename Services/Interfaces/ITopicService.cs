using BlogApp.Models;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;

namespace BlogApp.Services.Interfaces;

public interface ITopicService
{
    Task<IEnumerable<TopicResponseDto>> GetAllTopicsAsync();
    Task<TopicWithBlogsDto> GetTopicByIdAsync(int id);
    Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto dto, int authorId);
    Task DeleteTopicAsync(int id, string requesterRole);
}