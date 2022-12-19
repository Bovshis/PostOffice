using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PostService.Application.Users.Commands;
using PostService.Domain.Abstractions;
using UserService.Application.Users.Commands;

namespace PostService.Application.Messaging;

public class MessageProcessor : IMessageProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MessageProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<bool> ProcessMessage(string message, string routingKey, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        switch (routingKey)
        {
            case "user.create":
            {
                var user = JsonConvert.DeserializeObject<CreateUserCommand>(message)!;
                return await mediator.Send(user, cancellationToken);
            }
            case "user.update":
            {
                var user = JsonConvert.DeserializeObject<UpdateUserCommand>(message)!;
                return await mediator.Send(user, cancellationToken);
            }
            case "user.delete":
            {
                var user = JsonConvert.DeserializeObject<DeleteUserCommand>(message)!;
                return await mediator.Send(user, cancellationToken);
            }
            default:
                throw new ArgumentException("Unsupported routing key type.");
        }
    }
}