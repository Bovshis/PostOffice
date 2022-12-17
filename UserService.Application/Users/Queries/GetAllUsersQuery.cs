using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<User>>;