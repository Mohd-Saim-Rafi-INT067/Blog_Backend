using System.Security.Claims;
using BlogApp.DTOs.Blogs;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly ICommentService _commentService;
    public BlogController(IBlogService blogService, ICommentService commentService)
    {
        _blogService = blogService;
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogs(string ? title ) =>
        Ok(await _blogService.GetAllPublishedBlogsAsync(title));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        try
        {
            return Ok(await _blogService.GetBlogByIdAsync(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBlogBySlug(string slug)
    {
        try
        {
            return Ok(await _blogService.GetBlogBySlugAsync(slug));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
    }

    [HttpPost]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> CreateBlog(CreateBlogDto dto)
    {
        var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var result =  await _blogService.CreateBlogAsync(dto, authorId);
            return CreatedAtAction(nameof(GetBlogById), new { id = result.Id }, result);
            
        }catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> UpdateBlog(int id, UpdateBlogDto dto)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requesterRole = User.FindFirstValue(ClaimTypes.Role)!;
        try
        {
            return Ok(await _blogService.UpdateBlogAsync(id, dto, requesterId, requesterRole));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpPatch("{id}/publish")]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> PublishBlog(int id, PublishBlogDto dto)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requesterRole = User.FindFirstValue(ClaimTypes.Role)!;
        try
        {
            return Ok(await _blogService.PublishBlogAsync(id, dto.IsPublished, requesterId, requesterRole));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requesterRole = User.FindFirstValue(ClaimTypes.Role)!;
        try
        {
            await _blogService.DeleteBlogAsync(id, requesterId, requesterRole);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
    
    [HttpGet("{blogId}/comments")]
    public async Task<IActionResult> GetComments(int blogId) =>
        Ok(await _commentService.GetCommentsByBlogIdAsync(blogId));

    [HttpPost("{blogId}/comments")]
    [Authorize]
    public async Task<IActionResult> AddComment(int blogId, [FromBody] DTOs.Comments.CreateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try { return Ok(await _commentService.CreateCommentAsync(blogId, dto, userId)); }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPatch("{blogId}/comments/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment(int blogId, int id, [FromBody] DTOs.Comments.UpdateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        try { return Ok(await _commentService.UpdateCommentAsync(blogId, id, dto, userId, role)); }
        catch (UnauthorizedAccessException) { return Forbid(); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("{blogId}/comments/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(int blogId, int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        try { await _commentService.DeleteCommentAsync(blogId, id, userId, role); return NoContent(); }
        catch (UnauthorizedAccessException) { return Forbid(); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

}