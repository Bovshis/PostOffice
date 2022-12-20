using MediatR;

namespace PostService.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
}