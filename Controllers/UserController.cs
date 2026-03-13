using System.Security.Claims;
using BlogApp.DTOs.Users;
using BlogApp.Models;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var users = await _userService.GetAllUsersAsync(role);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try { return Ok(await _userService.GetUserByIdAsync(id)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        try { return Ok(await _userService.UpdateUserAsync(id, dto, requesterId, role)); }
        catch (UnauthorizedAccessException ) { return Forbid(); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        try
        {
            await _userService.DeleteUserAsync(id, requesterId, role);
            return NoContent();
        }
        catch (UnauthorizedAccessException) { return Forbid(); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpGet("{id}/blogs")]
    public async Task<IActionResult> GetUserBlogs(int id,
        [FromQuery] bool? isPublished)
    {
        var blogs = await _blogService.GetBlogsByAuthorAsync(id, isPublished);
        return Ok(blogs);
    }
}