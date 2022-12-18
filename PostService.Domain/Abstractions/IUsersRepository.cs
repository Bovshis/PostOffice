using PostService.Domain.Entities;

namespace PostService.Domain.Abstractions;

public interface IUsersRepository
{
    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}