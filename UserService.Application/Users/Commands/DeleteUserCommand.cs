using MediatR;

namespace UserService.Application.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<bool>;