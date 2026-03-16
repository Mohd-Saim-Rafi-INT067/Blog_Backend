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
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subService;
    public SubscriptionsController(ISubscriptionService subService)
    {
        _subService = subService;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMySubscriptions()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _subService.GetMySubscriptionsAsync(userId);
        return Ok(ApiResponseDto<IEnumerable<SubscriptionResponseDto>>.Ok(result, "Subscriptions fetched successfully."));
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetAuthorSubscribers(int authorId) {
        var result = await _subService.GetAuthorSubscribersAsync(authorId);
        return Ok(ApiResponseDto<IEnumerable<SubscriberResponseDto>>.Ok(result, "Subscribers fetched successfully."));
    }
    
    [HttpPost("{authorId}")]
    [Authorize]
    public async Task<IActionResult> SubscribeToAuthor(int authorId)
    {
        var subscriberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _subService.SubscribeAsync(subscriberId, authorId);
        return Ok(ApiResponseDto<string>.Ok("Subscribed successfully."));
        
    }

    [HttpDelete("{authorId}")]
    [Authorize]
    public async Task<IActionResult> UnsubscribeFromAuthor(int authorId)
    {
        var subscriberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _subService.UnsubscribeAsync(subscriberId, authorId);
        return Ok(ApiResponseDto<string>.Ok("Unsubscribed successfully."));
        
    }
}