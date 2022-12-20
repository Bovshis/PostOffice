using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
}