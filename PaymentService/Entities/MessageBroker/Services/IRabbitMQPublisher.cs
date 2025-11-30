namespace Entities.MessageBroker.Services
{
    public interface IRabbitMQPublisher<T>
    {
        Task PublishMessageAsync(T message, string queueName);
    }
}
