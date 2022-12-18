using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PostService.Application.Users.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.Formats.Asn1.AsnWriter;

namespace PostService.Application.Users;

public class RabbitMqUserSubscriber : BackgroundService
{
    private readonly IModel _channel;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private const string QueueName = "user.postservice";

    public RabbitMqUserSubscriber(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (moduleHandle, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            var user = JsonConvert.DeserializeObject<CreateUserCommand>(message)!;
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(user, stoppingToken);
        };

        _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        return Task.FromResult(Task.CompletedTask);
    }
}