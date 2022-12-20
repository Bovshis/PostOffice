using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Dtos;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserPublisher _userPublisher;

    public UpdateUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork, IUserPublisher userPublisher)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _userPublisher = userPublisher;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
        };
        var isUpdated = await _usersRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userPublishDto = new UserPublishDto()
        {
            Id = request.Id,
            Name = request.Name,
        };
        _userPublisher.PublishUser(userPublishDto, "user.update");

        return isUpdated;
    }
}