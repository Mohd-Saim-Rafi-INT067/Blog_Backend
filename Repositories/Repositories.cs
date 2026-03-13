using BlogApp.Models;
using BlogApp.Data;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<User>> GetAllAsync(UserRole? role = null)
    {
        var q = _db.Users.AsQueryable();
        if (role.HasValue) q = q.Where(u=>u.Role == role.Value);
       return await q.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id) => await _db.Users.FirstOrDefaultAsync(u=>u.Id == id);
    public async Task<User?> GetByEmailAsync(string email) => await _db.Users.FirstOrDefaultAsync(u=>u.Email == email);

    public async Task<User?> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task<User?> UpdateAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _db;
    public BlogRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Blog>> GetAllPublishedAsync(string? title = null)
    {
        var q = _db.Blogs.Include(b => b.Author).Include(b => b.Topic).Where(b => b.IsPublished);
        if (!string.IsNullOrEmpty(title))
        {
            q = q.Where(b=>b.Title.Contains(title));
        }
        return await q.ToListAsync();
    }

    public async Task<Blog?> GetByIdAsync(int id) => await _db.Blogs.Include(b => b.Author).Include(b => b.Topic).FirstOrDefaultAsync(b => b.Id == id);
    public async Task<Blog?> GetBySlugAsync(string slug) => await _db.Blogs.Include(b => b.Author).Include(b => b.Topic).FirstOrDefaultAsync(b => b.Slug == slug);

    public async Task<IEnumerable<Blog>> GetByAuthorAsync(int authorId, bool? isPublished)
    {
        var q = _db.Blogs.Include(b=>b.Topic).Include(b => b.Author).Where(b => b.AuthorId == authorId);
        if (isPublished.HasValue) q = q.Where(b => b.IsPublished == isPublished);
        return await q.ToListAsync();
    }

    public async Task<Blog> CreateAsync(Blog blog)
    {
        _db.Blogs.Add(blog);
        await _db.SaveChangesAsync();
        return blog;
    }

    public async Task<Blog> UpdateAsync(Blog blog)
    {
        blog.UpdatedAt = DateTime.UtcNow;
        _db.Blogs.Update(blog);
        await _db.SaveChangesAsync();
        return blog;
    }
    public async Task DeleteAsync(Blog blog)
    {
        _db.Blogs.Remove(blog);
        await _db.SaveChangesAsync();
    }
}

public class TopicRepository : ITopicRepository
{
    private readonly AppDbContext _db;
    public TopicRepository(AppDbContext db)
    {
        _db = db;
    }
    public Task<List<Topic>> GetAllAsync() =>
        _db.Topics.Include(t => t.Author).ToListAsync();
    
    async Task<IEnumerable<Topic>> ITopicRepository.GetAllAsync() => await GetAllAsync();

    public async Task<Topic?> GetByIdAsync(int id) => await _db.Topics.Include(t=>t.Author).FirstOrDefaultAsync(t=> t.Id == id);

    public Task<Topic?> GetByIdWithBlogsAsync(int id) =>
        _db.Topics.Include(t => t.Author).Include(t => t.Blogs).ThenInclude(b => b.Author)
                  .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Topic> CreateAsync(Topic topic)
    {
        _db.Topics.Add(topic);
        await _db.SaveChangesAsync();
        return topic;
    }

    public async Task DeleteAsync(Topic topic)
    {
        _db.Topics.Remove(topic);
        await _db.SaveChangesAsync();
    }
}

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _db;
    public CommentRepository(AppDbContext db)
    {
        _db = db;
    }
    public Task<List<Comment>> GetByBlogIdAsync(int blogId) =>
        _db.Comments.Include(c => c.User).Where(c => c.BlogId == blogId).ToListAsync();
    
    async Task<IEnumerable<Comment>> ICommentRepository.GetByBlogIdAsync(int blogId) => await GetByBlogIdAsync(blogId);

    public async Task<Comment?> GetByIdAsync(int id) => await _db.Comments.Include(c=> c.User).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Comment> CreateAsync(Comment comment)
    {
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        comment.UpdatedAt = DateTime.UtcNow;
        _db.Comments.Update(comment);
        await _db.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteAsync(Comment comment)
    {
        _db.Comments.Remove(comment);
        await _db.SaveChangesAsync();
    }
}

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
        await _db.Subscriptions.Include(s => s.Author).Where(s=> s.AuthorId == authorId).ToListAsync();

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