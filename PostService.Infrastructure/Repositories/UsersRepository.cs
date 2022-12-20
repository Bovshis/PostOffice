using Microsoft.EntityFrameworkCore;
using PostService.Domain.Abstractions;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(User updatedUser, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id, cancellationToken);
        if (user == null) return false;
        user.Name = updatedUser.Name;
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
        if (user == null) return false;
        _dbContext.Users.Remove(user);
        return true;
    }
}