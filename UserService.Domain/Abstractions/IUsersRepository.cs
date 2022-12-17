using UserService.Domain.Entities;

namespace UserService.Domain.Abstractions;

public interface IUsersRepository
{
    IEnumerable<User> GetAll();
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}