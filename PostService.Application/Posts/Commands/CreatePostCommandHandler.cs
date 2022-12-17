using MediatR;
using PostService.Domain.Abstractions;
using PostService.Domain.Entities;

namespace PostService.Application.Posts.Commands;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Post>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommandHandler(IPostsRepository postsRepository, IUnitOfWork unitOfWork)
    {
        _postsRepository = postsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post()
        {
            Title = request.Title,
            Content = request.Content,
            UserId = request.UserId,
        };

        var createdPost = await _postsRepository.Create(post);
        await _unitOfWork.SaveChanges(cancellationToken);
        return createdPost;
    }
}