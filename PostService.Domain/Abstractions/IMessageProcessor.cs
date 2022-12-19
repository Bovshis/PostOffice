namespace PostService.Domain.Abstractions;

public interface IMessageProcessor
{
    Task<bool> ProcessMessage(string message, string routingKey, CancellationToken cancellationToken);
}