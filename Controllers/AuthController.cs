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
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        
        var result = await _authService.RegisterAsync(dto);
        return Ok(ApiResponseDto<AuthResponseDto>.Ok(result, "Registered Successfully"));
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {

        var result = await _authService.LoginAsync(dto);
        return Ok(ApiResponseDto<AuthResponseDto>.Ok(result, "Login successful"));
        
    }


    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout() =>
        Ok(ApiResponseDto<object>.Ok(null, "Logged out successfully"));
    //frontend will delete the token on logout, so no server-side action is needed for stateless JWT auth

}