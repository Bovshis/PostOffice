using MediatR;
using PostService.Domain.Entities;

namespace PostService.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Post>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}