using System.ComponentModel.DataAnnotations;
using BlogApp.Models;
namespace BlogApp.DTOs.Users;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class UpdateUserDto
{
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
    public string? Username{ get; set; }
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
}