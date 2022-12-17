using PostService.Domain.Abstractions;

namespace PostService.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _dbContext;

    public UnitOfWork(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}