using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<User>>;