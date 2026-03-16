using BlogApp.Models;
namespace BlogApp.Repositories.Interfaces;

public interface ITopicRepository
{
    Task<IEnumerable<Topic>> GetAllAsync();//returns all topics
    Task<Topic?> GetByIdAsync(int id);//return a topic by id
    Task<Topic?> GetByIdWithBlogsAsync(int id);//all the blogs under a topic
    Task<Topic> CreateAsync(Topic topic);//create a new topic
    Task DeleteAsync(Topic topic);//delete a topic
}