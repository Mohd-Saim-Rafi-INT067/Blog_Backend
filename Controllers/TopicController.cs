using System.Security.Claims;
using BlogApp.DTOs.Topics;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    
    private readonly ITopicService _topicService;
    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTopics() =>
        Ok(await _topicService.GetAllTopicsAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopicById(int id)
    {
        try { return Ok (await _topicService.GetTopicByIdAsync(id)); }
        catch (KeyNotFoundException ex) { return NotFound(new {message = ex.Message}); }
    }

    [HttpPost]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> CreateTopic([FromBody] CreateTopicDto dto)
    {
        var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var result = await _topicService.CreateTopicAsync(dto, authorId);
            return CreatedAtAction(nameof(GetTopicById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTopic(int id)
    {
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        try {await _topicService.DeleteTopicAsync(id,role); return NoContent(); }
        catch (UnauthorizedAccessException) { return Forbid(); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

}

