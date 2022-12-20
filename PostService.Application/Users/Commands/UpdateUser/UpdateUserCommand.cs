using MediatR;

namespace PostService.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(int Id, string Name) : IRequest<bool>;