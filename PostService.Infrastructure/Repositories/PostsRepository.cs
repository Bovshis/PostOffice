using Microsoft.EntityFrameworkCore;
using PostService.Domain.Abstractions;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Repositories;

public class PostsRepository : IPostsRepository
{
    private readonly ApplicationContext _dbContext;

    public PostsRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Post> GetAll()
    {
        return _dbContext.Posts.Include(p => p.User).AsEnumerable();
    }

    public async Task<Post> Create(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
        return post;
    }
}