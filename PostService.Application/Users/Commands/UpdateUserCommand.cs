using MediatR;

namespace PostService.Application.Users.Commands;

public record UpdateUserCommand(int Id, string Name) : IRequest<bool>;