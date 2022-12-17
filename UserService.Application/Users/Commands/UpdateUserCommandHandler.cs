using MediatR;
using UserService.Domain.Abstractions;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
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
        return isUpdated;
    }
}