using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Dtos;

namespace UserService.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserPublisher _userPublisher;

    public DeleteUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork, IUserPublisher userPublisher)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _userPublisher = userPublisher;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await _usersRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userPublishDto = new UserPublishDto()
        {
            Id = request.Id
        };
        _userPublisher.PublishUser(userPublishDto, "user.delete");
        return isDeleted;
    }
}