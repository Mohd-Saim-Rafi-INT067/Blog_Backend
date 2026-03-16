using BlogApp.Models;
using BlogApp.Data;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories;



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

    public async Task<Comment?> CreateAsync(Comment comment)
    {
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(Comment comment)
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
