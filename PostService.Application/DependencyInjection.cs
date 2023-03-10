using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PostService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        return serviceCollection;
    }
}