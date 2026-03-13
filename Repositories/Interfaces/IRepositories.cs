using BlogApp.Models;
namespace BlogApp.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(UserRole? role = null); //returns all users
    Task<User?> GetByIdAsync(int id);//return a user by id
    Task<User?> GetByEmailAsync(string email);//return a user by email
    Task<User?> CreateAsync(User user);//create a new user
    Task<User?> UpdateAsync(User user);//update an existing user
    Task DeleteAsync(User user);//delete a user
}

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

public interface ITopicRepository
{
    Task<IEnumerable<Topic>> GetAllAsync();//returns all topics
    Task<Topic?> GetByIdAsync(int id);//return a topic by id
    Task<Topic?> GetByIdWithBlogsAsync(int id);//all the blogs under a topic
    Task<Topic> CreateAsync(Topic topic);//create a new topic
    Task DeleteAsync(Topic topic);//delete a topic
}

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId);//returns all comments under a blog
    Task<Comment?> GetByIdAsync(int id);//return a comment by id
    Task<Comment?> CreateAsync(Comment comment);//create a new comment
    Task<Comment?> UpdateAsync(Comment comment);//update an existing comment
    Task DeleteAsync(Comment comment);//delete a comment
}

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetMySubscriptionsAsync(int subscriberId);//returns all subscriptions for a user
    Task<IEnumerable<Subscription>> GetAuthorSubscribersAsync(int authorId);//returns all subscribers for an author
    Task<Subscription?> GetAsync(int subscriberId, int authorId);//Returns a specific subscription by subscriberId and authorId
    Task<Subscription> CreateAsync(Subscription subscription);//create a new subscription
    Task DeleteAsync(Subscription subscription);//delete a subscription
}