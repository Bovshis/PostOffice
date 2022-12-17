using Microsoft.EntityFrameworkCore;
using System.Threading;
using UserService.Domain.Abstractions;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users.AsEnumerable();
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        return user;
    }

    public async Task<bool> UpdateAsync(User updatedUser, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id, cancellationToken);
        if (user == null) return false;
        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
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