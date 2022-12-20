using MediatR;
using PostService.Domain.Entities;

namespace PostService.Application.Posts.Queries.GetAllPosts;

public record GetAllPostsQuery : IRequest<IEnumerable<Post>>;