using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository)
    {
        _unitOfWork = unitOfWork;
        _usersRepository = usersRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
        };
        var res = await _usersRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return res;
    }
}