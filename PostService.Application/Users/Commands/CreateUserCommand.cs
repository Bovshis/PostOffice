using MediatR;

namespace PostService.Application.Users.Commands;

public class CreateUserCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
}