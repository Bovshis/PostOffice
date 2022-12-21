using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application;
using UserService.Application.Users.Publishers;
using UserService.Domain.Abstractions;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;
using UserService.Middlewares;

namespace UserService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

        builder.Services.AddTransient<ExceptionHandlingMiddleware>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddSingleton<IUserPublisher, RabbitMqUserPublisher>();

        builder.Services.AddApplicationLayer();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}