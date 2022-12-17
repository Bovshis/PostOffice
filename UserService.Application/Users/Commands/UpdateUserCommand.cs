using MediatR;

namespace UserService.Application.Users.Commands;

public record UpdateUserCommand(int Id, string Name, string Email) : IRequest<bool>;