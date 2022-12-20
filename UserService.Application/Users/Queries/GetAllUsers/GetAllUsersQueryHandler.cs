using System.Runtime.CompilerServices;
using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUsersRepository _usersRepository;

    public GetAllUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_usersRepository.GetAll());
    }
}