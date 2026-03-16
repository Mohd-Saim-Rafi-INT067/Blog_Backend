using System.Security.Claims;
using BlogApp.DTOs.Auth;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.DTOs.Users;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogApp.DTOs;

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
    public async Task<IActionResult> GetAllTopics() {
        var result = await _topicService.GetAllTopicsAsync();
        return Ok(ApiResponseDto<IEnumerable<TopicResponseDto>>.Ok(result, "Topics fetched successfully."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopicById(int id)
    {
        var result = await _topicService.GetTopicByIdAsync(id);
        return Ok(ApiResponseDto<TopicResponseDto>.Ok(result, "Topic fetched successfully."));
    }

    [HttpPost]
    [Authorize(Roles = "Author,Admin")]
    public async Task<IActionResult> CreateTopic([FromBody] CreateTopicDto dto)
    {
        var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _topicService.CreateTopicAsync(dto, authorId);
        return CreatedAtAction(nameof(GetTopicById), new { id = result.Id }, ApiResponseDto<TopicResponseDto>.Created(result, "Topic created successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTopic(int id)
    {
        var role = User.FindFirstValue(ClaimTypes.Role)!;
        await _topicService.DeleteTopicAsync(id,role);
        return Ok(ApiResponseDto<string>.Ok(null, "Topic deleted successfully.")); 
    }
    
}

