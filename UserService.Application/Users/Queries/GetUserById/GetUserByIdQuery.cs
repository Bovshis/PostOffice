using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<User?>;