using System.ComponentModel.DataAnnotations;
using BlogApp.Models;

namespace BlogApp.DTOs.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.User;
}

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}

public class AuthResponseDto
{
    public string AccessToken { get; set; } = null!;
    public UserInfoDto User { get; set; } = null!;
}

public class UserInfoDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
     public string Role { get; set; } = null!;
}