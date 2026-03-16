using System.Security.Claims;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;
using BlogApp.DTOs;

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
    public async Task<IActionResult> GetAllBlogs(string ? title ){
        var result = await _blogService.GetAllPublishedBlogsAsync(title);
        return Ok(ApiResponseDto<IEnumerable<BlogResponseDto>>.Ok(result, "Blogs retrieved successfully"));
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        var result = await _blogService.GetBlogByIdAsync(id);
        return Ok(ApiResponseDto<BlogResponseDto>.Ok(result, "Blog retrieved successfully"));
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBlogBySlug(string slug)
    {
        var result = await _blogService.GetBlogBySlugAsync(slug);
        return Ok(ApiResponseDto<BlogResponseDto>.Ok(result, "Blog retrieved successfully"));
    }

    [HttpPost]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto dto)
    {
        var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result =  await _blogService.CreateBlogAsync(dto, authorId);
        return CreatedAtAction(nameof(GetBlogById), new { id = result.Id }, ApiResponseDto<BlogResponseDto>.Created(result, "Blog created successfully"));
        
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> UpdateBlog(int id, [FromBody] UpdateBlogDto dto)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requesterRole = User.FindFirstValue(ClaimTypes.Role)!;
        var result = await _blogService.UpdateBlogAsync(id, dto, requesterId, requesterRole);
        return Ok(ApiResponseDto<BlogResponseDto>.Ok(result, "Blog updated successfully"));
    }

    [HttpPatch("{id}/publish")]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> PublishBlog(int id, [FromBody] PublishBlogDto dto)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requesterRole = User.FindFirstValue(ClaimTypes.Role)!;
        var result = await _blogService.PublishBlogAsync(id, dto.IsPublished, requesterId, requesterRole);
        return Ok(ApiResponseDto<BlogResponseDto>.Ok(result,"Blog publish status updated successfully."));
        
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var requesterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var requesterRole = User.FindFirstValue(ClaimTypes.Role)!;
        await _blogService.DeleteBlogAsync(id, requesterId, requesterRole);
        return Ok(ApiResponseDto<string>.Ok(null, "Blog deleted successfully"));
        
    }
    
    [HttpGet("{blogId}/comments")]
    public async Task<IActionResult> GetComments(int blogId)
    {
        var result = await _commentService.GetCommentsByBlogIdAsync(blogId);
        return Ok(ApiResponseDto<IEnumerable<CommentResponseDto>>.Ok(result, "Comments retrieved successfully"));
    }

    [HttpPost("{blogId}/comments")]
    [Authorize]
    public async Task<IActionResult> AddComment(int blogId, [FromBody] CreateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _commentService.CreateCommentAsync(blogId, dto, userId);
        return Ok(ApiResponseDto<CommentResponseDto>.Ok(result, "Comment added successfully")); 
    }

    [HttpPatch("{blogId}/comments/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment(int blogId, int id, [FromBody] UpdateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        var result = await _commentService.UpdateCommentAsync(blogId, id, dto, userId, role);
        return Ok(ApiResponseDto<CommentResponseDto>.Ok(result, "Comment updated successfully"));
    }

    [HttpDelete("{blogId}/comments/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(int blogId, int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        await _commentService.DeleteCommentAsync(blogId, id, userId, role); 
        return Ok(ApiResponseDto<string>.Ok(null, "Comment deleted successfully")); 
    }
    
}