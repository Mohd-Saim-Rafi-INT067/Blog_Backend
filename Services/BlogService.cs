using BlogApp.DTOs.Blogs;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _repo;
    public BlogService(IBlogRepository repo)
    {
        _repo = repo;
    } 
    public async Task<IEnumerable<BlogResponseDto>> GetAllPublishedBlogsAsync(string? title = null)
    {
        var blogs = await _repo.GetAllPublishedAsync(title);
        return blogs.Select(MapToDto);
    }
    public async Task<BlogResponseDto> GetBlogByIdAsync(int id)
    {
        var blog = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Blog not found.");
        return MapToDto(blog);
    }
    public async Task<BlogResponseDto> GetBlogBySlugAsync(string slug)
    {
        var blog = await _repo.GetBySlugAsync(slug) ?? throw new KeyNotFoundException("Blog not found!");
        return MapToDto(blog);
    }
    public async Task<IEnumerable<BlogResponseDto>> GetBlogsByAuthorAsync(int authorId, bool? isPublished)
    {
        var blog = await _repo.GetByAuthorAsync(authorId, isPublished);
        return blog.Select(MapToDto);
    }
    public async Task<BlogResponseDto> CreateBlogAsync(CreateBlogDto dto, int authorId)
    {
        var slug = string.IsNullOrWhiteSpace(dto.Slug) ? GenerateSlug(dto.Title) : dto.Slug;
        var blog = new Blog
        {
            Title = dto.Title,
            Content = dto.Content,
            BannerImage = dto.BannerImage,
            IsPublished = dto.IsPublished,
            PublishedAt = dto.IsPublished ? DateTime.UtcNow : null,
            TopicId = dto.TopicId,
            AuthorId = authorId,
            Slug = slug
        };

        await _repo.CreateAsync(blog);
        var created = await _repo.GetByIdAsync(blog.Id) ?? throw new KeyNotFoundException("Blog not found.");
        return MapToDto(created);
        
    }
    public async Task<BlogResponseDto> UpdateBlogAsync(int id, UpdateBlogDto dto, int requesterId, string requesterRole)
    {
        var blog = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Blog not found!");
        if (blog.AuthorId != requesterId && requesterRole != "Admin")
            throw new UnauthorizedAccessException("You are not authorized to update this blog.");

        if (dto.Title != null) blog.Title = dto.Title;
        if (dto.Content != null) blog.Content = dto.Content;
        if (dto.BannerImage != null) blog.BannerImage = dto.BannerImage;
        if (dto.IsPublished.HasValue) blog.IsPublished = dto.IsPublished.Value;
        if (dto.TopicId.HasValue) blog.TopicId = dto.TopicId.Value;
        if (dto.Slug != null) blog.Slug = dto.Slug;

        await _repo.UpdateAsync(blog);
        return MapToDto(blog);
    }

    public async Task<BlogResponseDto> PublishBlogAsync(int id, bool isPublished, int requesterId, string requesterRole)
    {
        var blog = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Blog not found!");
        if (blog.AuthorId != requesterId && requesterRole != "Admin")
            throw new UnauthorizedAccessException("You are not authorized to publish/unpublish this blog.");

        blog.IsPublished = isPublished;
        blog.PublishedAt = isPublished ? DateTime.UtcNow : null;

        await _repo.UpdateAsync(blog);
        return MapToDto(blog);
    }

    public async Task DeleteBlogAsync(int id, int requesterId, string requesterRole)
    {
        var blog = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Post not found.");
        if (blog.AuthorId != requesterId && requesterRole != "Admin")
        {
            throw new UnauthorizedAccessException("Access denied.");
        }
        await _repo.DeleteAsync(blog);
    }

    private static string GenerateSlug(string title)=>
        title.ToLower().Replace(' ','-').Replace("'","").Replace("\"","") + "-" + Guid.NewGuid().ToString()[..6];

    private static BlogResponseDto MapToDto(Blog b) => new()
    {
        Id = b.Id, Title = b.Title, Content = b.Content,
        BannerImage = b.BannerImage, Slug = b.Slug,
        IsPublished = b.IsPublished, PublishedAt = b.PublishedAt,
        CreatedAt = b.CreatedAt,
        Author = new AuthorDto { Id = b.Author.Id, Username = b.Author.Username },
        Topic = new TopicDto { Id = b.Topic.Id, Name = b.Topic.Name }
    };
}