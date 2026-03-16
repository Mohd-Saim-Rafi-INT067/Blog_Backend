using System.Security.Claims;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;
using BlogApp.Models;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogApp.DTOs;

namespace BlogApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IBlogService _blogService;

    public UserController(IUserService userService, IBlogService blogService)
    {
        _userService = userService;
        _blogService = blogService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserRole? role)
    {
        var result = await _userService.GetAllUsersAsync(role);
        return Ok(ApiResponseDto<IEnumerable<UserResponseDto>>.Ok(result, "Users fetched successfully."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(ApiResponseDto<UserResponseDto>.Ok(result, "User fetched successfully."));
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        var result = await _userService.UpdateUserAsync(id, dto, requesterId, role);
        return Ok(ApiResponseDto<UserResponseDto>.Ok(result, "User updated successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        await _userService.DeleteUserAsync(id, requesterId, role);
        return Ok(ApiResponseDto<string>.Ok("User deleted successfully."));
    }

    [HttpGet("{id}/blogs")]
    public async Task<IActionResult> GetUserBlogs(int id,[FromQuery] bool? isPublished)
    {
        var blogs = await _blogService.GetBlogsByAuthorAsync(id, isPublished);
        return Ok(ApiResponseDto<IEnumerable<BlogResponseDto>>.Ok(blogs, "User blogs fetched successfully."));
    }
}