
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _repo;
    public SubscriptionService(ISubscriptionRepository repo)
    {
        _repo = repo;
    }
    public async Task<IEnumerable<SubscriptionResponseDto>> GetMySubscriptionsAsync(int userId)
    {
        var subs = await _repo.GetMySubscriptionsAsync(userId);
        return subs.Select(s => new SubscriptionResponseDto
        {
            AuthorId = s.AuthorId,
            Username = s.Author?.Username ?? "",
            SubscribedSince = s.CreatedAt
        });
    }

    public async Task<IEnumerable<SubscriberResponseDto>> GetAuthorSubscribersAsync(int authorId)
    {
        var subs = await _repo.GetAuthorSubscribersAsync(authorId);
        return subs.Select(s => new SubscriberResponseDto
        {
            SubscriberId = s.SubscriberId,
            Username = s.Subscriber?.Username ?? "",
            CreatedAt = s.CreatedAt
        });
    }
    public async Task SubscribeAsync(int subscriberId, int authorId)
    {
        if (subscriberId == authorId)
            throw new Exception("You cannot subscribe to yourself.");

        var existingSub = await _repo.GetAsync(subscriberId, authorId);
        if (existingSub != null)
            throw new Exception("You are already subscribed to this author.");

        await _repo.CreateAsync(new Subscription{SubscriberId = subscriberId, AuthorId = authorId });
    }

    public async Task UnsubscribeAsync(int subscriberId, int authorId)
    {
        var subs = await _repo.GetAsync(subscriberId, authorId) ?? throw new KeyNotFoundException("Subscription not found.");
        await _repo.DeleteAsync(subs);
    }
}