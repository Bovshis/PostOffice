using System.Text;
using Microsoft.Extensions.Hosting;
using PostService.Domain.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PostService.Application.Messaging;

public class RabbitMqUserSubscriber : BackgroundService
{
    private readonly IModel _channel;
    private readonly IMessageProcessor _messageProcessor;
    private const string QueueName = "user.postservice";

    public RabbitMqUserSubscriber(IMessageProcessor messageProcessor)
    {
        _messageProcessor = messageProcessor;

        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            var routingKey = ea.RoutingKey;
            var response = await _messageProcessor.ProcessMessage(message, routingKey, stoppingToken);
        };

        _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        return Task.FromResult(Task.CompletedTask);
    }
}