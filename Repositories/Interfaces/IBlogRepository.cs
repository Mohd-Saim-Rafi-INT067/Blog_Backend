using BlogApp.Models;
namespace BlogApp.Repositories.Interfaces;

public interface IBlogRepository
{
    Task<IEnumerable<Blog>> GetAllPublishedAsync(string? title = null);//returns all published blogs
    Task<Blog?> GetByIdAsync(int id);//returns a blog by id
    Task<Blog?> GetBySlugAsync(string slug);//returns a blog by slug
    Task<IEnumerable<Blog>> GetByAuthorAsync(int authorId, bool? isPublished= null);//returns all blogs by author
    Task<Blog> CreateAsync(Blog blog);//create a new blog
    Task<Blog> UpdateAsync(Blog blog);//update an existing blog
    Task DeleteAsync(Blog blog);//delete a blog
}