using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<User> Users => Set<User>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u=>u.Id);
            e.HasIndex(u=>u.Email).IsUnique();
            e.Property(u => u.Role).HasConversion<string>();
        });

        //Topic
        modelBuilder.Entity<Topic>(e =>
        {
           e.HasKey(t=>t.Id);
           e.HasIndex(t=>t.Name).IsUnique();
           e.HasOne(t=>t.Author).WithMany(u => u.Topics).HasForeignKey(t=>t.AuthorId).OnDelete(DeleteBehavior.Restrict);
        });

        //Blog
        modelBuilder.Entity<Blog>(e =>
        {
           e.HasKey(b=>b.Id);
           e.HasIndex(b => b.Slug).IsUnique();
           e.HasOne(b=>b.Author).WithMany(u=>u.Blogs).HasForeignKey(b=>b.AuthorId).OnDelete(DeleteBehavior.Restrict);
           e.HasOne(b=>b.Topic).WithMany(t=>t.Blogs).HasForeignKey(b=>b.TopicId).OnDelete(DeleteBehavior.Restrict);
        });

        //Comment
        modelBuilder.Entity<Comment>(e =>
        {
           e.HasKey(c => c.Id);
           e.HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
           e.HasOne(c => c.Blog).WithMany(b => b.Comments).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.Cascade);
        });

        //subscriptions
        modelBuilder.Entity<Subscription>(e =>
        {
            e.HasKey(s => new { s.SubscriberId, s.AuthorId });
            e.HasOne(s => s.Subscriber)
             .WithMany(u => u.Subscriptions)
             .HasForeignKey(s => s.SubscriberId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(s => s.Author)
             .WithMany(u => u.Subscribers)
             .HasForeignKey(s => s.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed Admin user
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@blogapp.com",
            PasswordHash = "$2a$12$PozmYywyVjw7AwcTQRlHvevSsG.FHPPgsVsABvbZOMwn7Xo2D/OPK", //admin
            Role = UserRole.Admin,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}