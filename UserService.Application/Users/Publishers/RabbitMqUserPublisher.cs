using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using UserService.Domain.Abstractions;
using UserService.Domain.Dtos;

namespace UserService.Application.Users.Publishers;

public class RabbitMqUserPublisher : IUserPublisher
{
    private readonly IModel _channel;
    private const string ExchangeName = "user.fanout";
    private const string RoutingKeyName = "";

    public RabbitMqUserPublisher()
    {
        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public void PublishUser(UserPublishDto userPublishDto)
    {
        if (_channel.IsClosed) return;

        var message = JsonConvert.SerializeObject(userPublishDto);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKeyName, basicProperties: null, body: body);
    }
}