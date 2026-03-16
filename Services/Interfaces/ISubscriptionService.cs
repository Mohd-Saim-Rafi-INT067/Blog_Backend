using BlogApp.Models;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;

namespace BlogApp.Services.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionResponseDto>> GetMySubscriptionsAsync(int userId);
    Task<IEnumerable<SubscriberResponseDto>> GetAuthorSubscribersAsync(int authorId);
    Task SubscribeAsync(int subscriberId, int authorId);
    Task UnsubscribeAsync(int subscriberId, int authorId);
}