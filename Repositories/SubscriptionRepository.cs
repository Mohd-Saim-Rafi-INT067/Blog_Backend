using BlogApp.Models;
using BlogApp.Data;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _db;
    public SubscriptionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Subscription>> GetMySubscriptionsAsync(int subscriberId) =>
        await _db.Subscriptions.Include(s => s.Author).Where(s=> s.SubscriberId == subscriberId).ToListAsync();

    public async Task<IEnumerable<Subscription>> GetAuthorSubscribersAsync(int authorId) =>
        await _db.Subscriptions.Include(s => s.Subscriber).Where(s=> s.AuthorId == authorId).ToListAsync();

    public async Task<Subscription?> GetAsync(int subscriberId, int authorId)=>
        await _db.Subscriptions.FirstOrDefaultAsync(s => s.SubscriberId == subscriberId && s.AuthorId == authorId);
    
    public async Task<Subscription> CreateAsync(Subscription sub)
    {
        _db.Subscriptions.Add(sub);
        await _db.SaveChangesAsync();
        return sub;
    }
    public async Task DeleteAsync(Subscription sub)
    {
        _db.Subscriptions.Remove(sub);
        await _db.SaveChangesAsync();
    }
    

    

}