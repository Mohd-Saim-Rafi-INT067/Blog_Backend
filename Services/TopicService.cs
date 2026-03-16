using BlogApp.DTOs.Comments;
using BlogApp.DTOs.Blogs;
using BlogApp.DTOs.Subscriptions;
using BlogApp.DTOs.Topics;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _repo;
    public TopicService(ITopicRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TopicResponseDto>> GetAllTopicsAsync()
    {
        var topics = await _repo.GetAllAsync();
        return topics.Select(MapToDto);
    }

    public async Task<TopicWithBlogsDto> GetTopicByIdAsync(int id)
    {
        var topic = await _repo.GetByIdWithBlogsAsync(id) ?? throw new KeyNotFoundException("Topic not found");
        return new TopicWithBlogsDto
        {
            Id = topic.Id,
            Name = topic.Name, 
            AuthorId = topic.AuthorId,
            CreatedBy = topic.Author.Username,
            CreatedAt = topic.CreatedAt,
            Blogs = topic.Blogs.Select(p => new BlogResponseDto
            {
                Id = p.Id, Title = p.Title, Slug = p.Slug,
                IsPublished = p.IsPublished, CreatedAt = p.CreatedAt,
                Author = new AuthorDto { Id = p.Author.Id, Username = p.Author.Username },
                Topic = new TopicDto { Id = topic.Id, Name = topic.Name }
            }).ToList() 
        };
    }

    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto dto,int authorId)
    {
        var topic = new Topic { Name = dto.Name, AuthorId = authorId};
        await _repo.CreateAsync(topic);
        var created = await _repo.GetByIdAsync(topic.Id);
        return MapToDto(created!);
    }

    public async Task DeleteTopicAsync(int id, string requesterRole)
    {
        if (requesterRole != "Admin") throw new UnauthorizedAccessException("Admin only.");
        var topic = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Topic not found.");
        await _repo.DeleteAsync(topic);
    }
    private static TopicResponseDto MapToDto(Topic t) => new()
    {
        Id = t.Id, Name = t.Name, AuthorId = t.AuthorId,
        CreatedBy = t.Author?.Username ?? "", CreatedAt = t.CreatedAt
    };
}