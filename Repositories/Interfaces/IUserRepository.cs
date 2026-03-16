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