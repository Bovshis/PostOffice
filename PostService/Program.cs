using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Application;
using PostService.Domain.Abstractions;
using PostService.Infrastructure;
using PostService.Infrastructure.Repositories;

namespace PostService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
            builder.Services.AddApplicationLayer();
            builder.Services.AddScoped<IPostsRepository, PostsRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}