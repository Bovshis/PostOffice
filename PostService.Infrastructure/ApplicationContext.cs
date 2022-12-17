using Microsoft.EntityFrameworkCore;
using PostService.Domain.Entities;

namespace PostService.Infrastructure;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}