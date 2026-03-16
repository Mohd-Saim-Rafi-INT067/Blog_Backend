using BlogApp.Models;
namespace BlogApp.Repositories.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetMySubscriptionsAsync(int subscriberId);//returns all subscriptions for a user
    Task<IEnumerable<Subscription>> GetAuthorSubscribersAsync(int authorId);//returns all subscribers for an author
    Task<Subscription?> GetAsync(int subscriberId, int authorId);//Returns a specific subscription by subscriberId and authorId
    Task<Subscription> CreateAsync(Subscription subscription);//create a new subscription
    Task DeleteAsync(Subscription subscription);//delete a subscription
}