using UserService.Domain.Dtos;

namespace UserService.Domain.Abstractions;

public interface IUserPublisher
{
    void PublishUser(UserPublishDto userPublishDto, string routingKey);
}