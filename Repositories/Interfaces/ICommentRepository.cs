using BlogApp.Models;
namespace BlogApp.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId);//returns all comments under a blog
    Task<Comment?> GetByIdAsync(int id);//return a comment by id
    Task<Comment?> CreateAsync(Comment comment);//create a new comment
    Task<Comment?> UpdateAsync(Comment comment);//update an existing comment
    Task DeleteAsync(Comment comment);//delete a comment
}
