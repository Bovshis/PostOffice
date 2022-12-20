using Microsoft.EntityFrameworkCore;
using PostService.Domain.Abstractions;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Repositories;

public class PostsRepository : IPostsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Post> GetAll()
    {
        return _dbContext.Posts.Include(p => p.User).AsEnumerable();
    }

    public async Task<Post> CreateAsync(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
        return post;
    }
}