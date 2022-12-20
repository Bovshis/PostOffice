using MediatR;
using PostService.Domain.Abstractions;
using PostService.Domain.Entities;

namespace PostService.Application.Posts.Queries.GetAllPosts;

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<Post>>
{
    private readonly IPostsRepository _postsRepository;

    public GetAllPostsQueryHandler(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    public Task<IEnumerable<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_postsRepository.GetAll());
    }
}