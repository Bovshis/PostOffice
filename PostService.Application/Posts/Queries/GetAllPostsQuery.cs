using MediatR;
using PostService.Domain.Entities;

namespace PostService.Application.Posts.Queries;

public record GetAllPostsQuery : IRequest<IEnumerable<Post>>;