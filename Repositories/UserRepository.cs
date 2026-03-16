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
