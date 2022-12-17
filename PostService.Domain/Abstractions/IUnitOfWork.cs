namespace PostService.Domain.Abstractions;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken);
}