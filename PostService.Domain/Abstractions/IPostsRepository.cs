using PostService.Domain.Entities;

namespace PostService.Domain.Abstractions;

public interface IPostsRepository
{
    IEnumerable<Post> GetAll();
    Task<Post> Create(Post post);
}