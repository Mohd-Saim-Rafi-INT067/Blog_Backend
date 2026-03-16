using BlogApp.DTOs.Users;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(UserRole? role = null)
    {
        var users = await _userRepo.GetAllAsync(role);
        return users.Select(MapToDto);
    }
    public async Task<UserResponseDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id) ?? throw new Exception("User not found!");
        return MapToDto(user);
    }
    
    public async Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto dto, int requesterId, string requesterRole)
    {
        var user = await _userRepo.GetByIdAsync(id) ?? throw new Exception("User not found!");
        if (requesterRole != "Admin" && requesterId != id)
        {
            throw new UnauthorizedAccessException("Unauthorized!");
        }
        if (dto.Username != null) user.Username = dto.Username;
        if (dto.Email != null) user.Email = dto.Email;
        await _userRepo.UpdateAsync(user);
        return MapToDto(user);
    }

    public async Task DeleteUserAsync(int id, int requesterId, string requesterRole)
    {
        var user = await _userRepo.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found.");
        if (requesterId != id && requesterRole != "Admin"){
            throw new UnauthorizedAccessException("Access denied.");
        }
        await _userRepo.DeleteAsync(user);
    }

    private static UserResponseDto MapToDto(User u) => new()
    {
        Id = u.Id,
        Username = u.Username,
        Email = u.Email,
        Role = u.Role.ToString(),
        CreatedAt = u.CreatedAt
    };


}