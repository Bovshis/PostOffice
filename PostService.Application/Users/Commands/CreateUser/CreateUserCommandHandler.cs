using MediatR;
using PostService.Domain.Abstractions;
using PostService.Domain.Entities;

namespace PostService.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository)
    {
        _unitOfWork = unitOfWork;
        _usersRepository = usersRepository;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = request.Id,
            Name = request.Name,
        };
        var isCreated = await _usersRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return isCreated;
    }
}