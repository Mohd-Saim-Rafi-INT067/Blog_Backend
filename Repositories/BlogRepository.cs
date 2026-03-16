using BlogApp.Models;
using BlogApp.Data;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories;

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