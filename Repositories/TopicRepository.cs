using BlogApp.Models;
using BlogApp.Data;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories;


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