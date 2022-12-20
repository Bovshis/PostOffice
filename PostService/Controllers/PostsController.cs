using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Posts.Commands.CreatePost;
using PostService.Application.Posts.Queries.GetAllPosts;
using PostService.Application.Users.Commands.CreateUser;
using PostService.Domain.Entities;

namespace PostService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll(CancellationToken cancellationToken)
    {
        var posts = await _mediator.Send(new GetAllPostsQuery(), cancellationToken);
        return Ok(posts);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Post>> Create([FromBody] CreatePostCommand createPostCommand,
        CancellationToken cancellationToken)
    {
        var post = await _mediator.Send(createPostCommand, cancellationToken);
        return Ok(post);
    }

    [HttpPost("createu")]
    public async Task<ActionResult<bool>> CreateU([FromBody] CreateUserCommand createPostCommand,
        CancellationToken cancellationToken)
    {
        var post = await _mediator.Send(createPostCommand, cancellationToken);
        return Ok(post);
    }
}