using MediatR;

namespace PostService.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<bool>;