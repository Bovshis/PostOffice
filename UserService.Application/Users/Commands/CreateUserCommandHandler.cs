using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Dtos;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserPublisher _userPublisher;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUsersRepository usersRepository, IUserPublisher userPublisher)
    {
        _unitOfWork = unitOfWork;
        _usersRepository = usersRepository;
        _userPublisher = userPublisher;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
        };
        var createdUser = await _usersRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userPublishDto = new UserPublishDto()
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
        };
        _userPublisher.PublishUser(userPublishDto, "user.create");

        return createdUser;
    }
}