using BlogApp.Models;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;

namespace BlogApp.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(UserRole? role = null);
    Task<UserResponseDto> GetUserByIdAsync(int id);
    Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto dto, int requesterId, string requesterRole);
    Task DeleteUserAsync(int id, int requesterId, string requesterRole);
}