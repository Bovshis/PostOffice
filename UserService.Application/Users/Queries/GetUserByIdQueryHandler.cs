using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Queries;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserByIdQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _usersRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}