using System.Security.Claims;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(await _subService.GetMySubscriptionsAsync(userId));
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetAuthorSubscribers(int authorId) =>
        Ok(await _subService.GetAuthorSubscribersAsync(authorId));
    
    [HttpPost("{authorId}")]
    [Authorize]
    public async Task<IActionResult> SubscribeToAuthor(int authorId)
    {
        var subscriberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _subService.SubscribeAsync(subscriberId, authorId);
            return Ok(new { message = "Subscribed successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{authorId}")]
    [Authorize]
    public async Task<IActionResult> UnsubscribeFromAuthor(int authorId)
    {
        var subscriberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            await _subService.UnsubscribeAsync(subscriberId, authorId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }


}